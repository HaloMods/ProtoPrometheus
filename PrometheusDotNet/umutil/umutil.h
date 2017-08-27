// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the UMUTIL_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// UMUTIL_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef UMUTIL_EXPORTS
#define UMUTIL_API __declspec(dllexport)
#else
#define UMUTIL_API __declspec(dllimport)
#endif

typedef struct STRUCT_MAPFILE_HDR
{
  int  id;                    // 'head'
  int  Version;               // 5
  int  decomp_len;            // Actual len of decompressed data. Halo sticks garbage on the end so that the file is one of several fixed sizes (35, etc).
  int  Unknown1;
  int  TagIndexOffset;
  int  TagIndexMetaLength;
  int  Reserved1[2];          // both always 0x0
  char Name[32];
  char BuildDate[32];         // Year.Month.Day.Build - I guess they use this to make sure that a certain build will only open that build's map files, because this string is in the app too
  int  MapType;               // 0 = singleplayer, 1 = multiplayer, 2 = ui - this also determines the size of the cache file. UI = 35MB, multiplayer = 47MB, and singleplayer = 270MB
  int  Unknown4;
  int  Reserved2[485];
  int  Footer;                // 'foot'
}MAPFILE_HDR; /* header_t */ 

// This class is exported from the umutil.dll
class UMUTIL_API Cumutil {
public:
	Cumutil(void);
	// TODO: add your methods here.
};

extern UMUTIL_API int numutil;

UMUTIL_API int fnumutil(void);
