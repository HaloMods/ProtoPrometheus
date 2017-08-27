// umutil.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "umutil.h"
#include "zlib.h"
#include "stdio.h"
#include <sys/types.h>
#include <sys/stat.h>

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
    return TRUE;
}

//// This is an example of an exported variable
//UMUTIL_API int numutil=0;
//
//// This is an example of an exported function.
//UMUTIL_API int fnumutil(void)
//{
//	return 42;
//}
//
//// This is the constructor of a class that has been exported.
//// see umutil.h for the class definition
//Cumutil::Cumutil()
//{ 
//	return; 
//}
//
UMUTIL_API int CompressMapFile(char* infile, char* outfile)
{
  BYTE *pInBuf = NULL;
  BYTE *pOutBuf = NULL;
  HANDLE hInFile, hOutFile, hInSection, hOutSection;
  MAPFILE_HDR *pHeader = NULL;
  int result = Z_MEM_ERROR;
  unsigned long out_len;
  int actual_file_size = 0;
  int desired_file_size = 0;
  int pad_bytes = 0;
  bool bOverwriteOriginal = false;
  char* TmpFileName = "ctemp";
  int ret = -100;

  //check to see if we are overwriting the original
  if(strncmp(infile, outfile, 255) == 0)
    bOverwriteOriginal = true;
  
  struct _stat fileinfo;
  _stat(infile, &fileinfo);
  out_len = fileinfo.st_size - sizeof(MAPFILE_HDR);

  //FILE* plogfile = fopen("c:\umdebug.txt", "wb");
  //fprintf(plogfile, "output size = %d\n", out_len);
  //fclose(plogfile);

  // Use Filemapping to make the input file "look" like a normal buffer
  hInFile = CreateFile(infile, GENERIC_READ, 0, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0);

  hInSection = CreateFileMapping(hInFile,
                                 0,
                                 PAGE_READONLY,
                                 0,   //this is not a large file
                                 0,   //map the entire file
                                 0);  //it does not need a name - no interprocess comm here :)

  pInBuf = (BYTE*)MapViewOfFile(hInSection,
                                FILE_MAP_READ,
                                0,
                                0,
                                0);

  if(pInBuf)
  {
    // Do the same for the output "buffer" (make an output file)
    if(bOverwriteOriginal)
      hOutFile = CreateFile(TmpFileName, GENERIC_READ|GENERIC_WRITE, 0, 0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);
    else
      hOutFile = CreateFile(outfile, GENERIC_READ|GENERIC_WRITE, 0, 0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

    pHeader = (MAPFILE_HDR*)pInBuf;

    hOutSection = CreateFileMapping(hOutFile,
                                    0,
                                    PAGE_READWRITE,
                                    0,   //this is not a large file
                                    pHeader->decomp_len+2048,   //the decompressed length of the output file
                                    0);  //it does not need a name - no interprocess comm here :)

    pOutBuf = (BYTE*)MapViewOfFile(hOutSection,
                                   FILE_MAP_WRITE,
                                   0,
                                   0,
                                   0);

    if(!pOutBuf)
    {
      ret = -99;
      UnmapViewOfFile(pOutBuf);
      CloseHandle(hOutSection);
    }
    else
    {
      //write header
      memcpy(pOutBuf, pInBuf, sizeof(MAPFILE_HDR));

      //compress file data using zlib
      ret = compress(pOutBuf+2048,
                     &out_len,
                     pInBuf+2048,
                     pHeader->decomp_len-2048);

      int actual_file_size = out_len + 2048;
      int desired_file_size;
      int pad_bytes;

      pad_bytes = (0x800 - (actual_file_size % 0x800));
      desired_file_size = actual_file_size + pad_bytes;

      //truncate file to desired_file_size
      UnmapViewOfFile(pOutBuf);
      CloseHandle(hOutSection);
      SetFilePointer(hOutFile, desired_file_size, 0, FILE_BEGIN);      
      SetEndOfFile(hOutFile);
    }
    
    CloseHandle(hOutFile);
  }

  UnmapViewOfFile(pInBuf);
  CloseHandle(hInSection);
  CloseHandle(hInFile);
  
  if(bOverwriteOriginal)
  {
    remove(infile);
    rename(TmpFileName, outfile);
  }

  return(ret);
}


UMUTIL_API int DecompressMapFile(char* infile, char* outfile)
{
  BYTE *pInBuf = NULL;
  BYTE *pOutBuf = NULL;
  HANDLE hInFile, hOutFile, hInSection, hOutSection;
  MAPFILE_HDR *pHeader = NULL;
  DWORD InFileLen, InFileLenHigh, DecompLen;
  char* TmpFileName = "ctemp";
  bool bOverwriteOriginal = false;

  //check to see if we are overwriting the original
  if(strncmp(infile, outfile, 255) == 0)
    bOverwriteOriginal = true;

  int result = Z_MEM_ERROR;

  int k = sizeof(MAPFILE_HDR);
  // Use Filemapping to make the input file "look" like a normal buffer
  hInFile = CreateFile(infile,
                       GENERIC_READ,
                       0,             // do not share
                       0,
                       OPEN_EXISTING,
                       FILE_ATTRIBUTE_NORMAL,
                       0);

  hInSection = CreateFileMapping(hInFile,
                                 0,
                                 PAGE_READONLY,
                                 0,   //this is not a large file
                                 0,   //map the entire file
                                 0);  //it does not need a name - no interprocess comm here :)

  pInBuf = (BYTE*)MapViewOfFile(hInSection,
                                FILE_MAP_READ,
                                0,
                                0,
                                0);

  if(pInBuf)
  {
    // Do the same for the output "buffer" (make an output file)
    if(bOverwriteOriginal)
      hOutFile = CreateFile(TmpFileName, GENERIC_READ|GENERIC_WRITE, 0, 0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);
    else
      hOutFile = CreateFile(outfile, GENERIC_READ|GENERIC_WRITE, 0, 0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

    pHeader = (MAPFILE_HDR*)pInBuf;

    hOutSection = CreateFileMapping(hOutFile,
                                    0,
                                    PAGE_READWRITE,
                                    0,   //this is not a large file
                                    pHeader->decomp_len+2048,   //the decompressed length of the output file
                                    0);  //it does not need a name - no interprocess comm here :)

    pOutBuf = (BYTE*)MapViewOfFile(hOutSection,
                                   FILE_MAP_WRITE,
                                   0,
                                   0,
                                   0);

    if(!pOutBuf)
    {
      //g_pOutput->PostText("Could not expand cache.tmp.\r\nThere may not be enough hard drive space.\n", LOG_BLUE);
      //AfxMessageBox("Could not expand cache.tmp.  There may not be enough hard drive space.\n");
    }
    else
    {
      //CWaitCursor wait;

      InFileLen = GetFileSize(hInFile, &InFileLenHigh);
      DecompLen = pHeader->decomp_len;

      memcpy(pOutBuf, pHeader, sizeof(MAPFILE_HDR));

      result = uncompress(pOutBuf+2048,
                              &DecompLen,
                              pInBuf+2048,
                              InFileLen-2048);

    }
    
    UnmapViewOfFile(pOutBuf);
    CloseHandle(hOutSection);
    CloseHandle(hOutFile);
  }

  UnmapViewOfFile(pInBuf);
  CloseHandle(hInSection);
  CloseHandle(hInFile);

  if(bOverwriteOriginal)
  {
    remove(infile);
    rename(TmpFileName, outfile);
  }

  return(result);
}
