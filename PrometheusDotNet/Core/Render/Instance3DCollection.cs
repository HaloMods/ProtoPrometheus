using System;
using System.Diagnostics;
using System.Collections;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Prometheus.Core.Project;
using Prometheus.Core.Tags.Sbsp;

namespace Prometheus.Core.Render
{
  public enum MultiSelectMode{Disabled, FindEnd, Active};
  public class SelectBox2D
  {
    private int selectionBoxStartX;
    private int selectionBoxStartY;
    private int selectionBoxEndX;
    private int selectionBoxEndY;
    private MultiSelectMode multiSelectMode = MultiSelectMode.Disabled;
    private int activeClickCount = 0;

    public MultiSelectMode Mode
    {
      get{return multiSelectMode;}
    }
    public void MouseMove(int MouseX, int MouseY)
    {
      if(multiSelectMode == MultiSelectMode.FindEnd)
      {
        selectionBoxEndX = MouseX;
        selectionBoxEndY = MouseY;
      }
    }
    public void MouseUp(int MouseX, int MouseY)
    {
      switch(multiSelectMode)
      {
        case MultiSelectMode.Disabled:
          if(MdxRender.Input.StartSelectionBox)
          {
            selectionBoxStartX = MouseX;
            selectionBoxStartY = MouseY;
            selectionBoxEndX = MouseX;
            selectionBoxEndY = MouseY;
            multiSelectMode = MultiSelectMode.FindEnd;
          }
          break;

        case MultiSelectMode.FindEnd:
          selectionBoxEndX = MouseX;
          selectionBoxEndY = MouseY;
          multiSelectMode = MultiSelectMode.Active;
          break;

        case MultiSelectMode.Active:
          activeClickCount++;
          if(activeClickCount > 1)
          {
            activeClickCount = 0;
            multiSelectMode = MultiSelectMode.Disabled;
          }
          break;
      }
    }
    public ArrayList UpdateFrustumSelection(Instance3DCollection InstanceList)
    {
      ArrayList selected_items = new ArrayList();
      Plane[] fplanes;
      MdxRender.Camera.GetFrustumPlanes(selectionBoxStartX, selectionBoxStartY, 
        selectionBoxEndX, selectionBoxEndY, out fplanes);

      float test;

      //there seems to be a little bit of a plane calculation bug, objects within the bounding box are
      //not always selected perfectly
      bool bInsideFrustum = false;
      int sel_count = 0;
      for(int i=0; i<InstanceList.Count; i++)
      {
        bInsideFrustum = true;
        for(int p=0; p<fplanes.Length; p++)
          //for(int p=0; p<2; p++)
        {
          test = InstanceList[i].Translation.X*fplanes[p].A + 
            InstanceList[i].Translation.Y*fplanes[p].B + 
            InstanceList[i].Translation.Z*fplanes[p].C +
            fplanes[p].D;

          if(test < 0) //test for behind plane (outside of frustum) (normals point to inside of frustum)
          {
            bInsideFrustum = false;
            break;
          }
        }

        InstanceList[i].Selected = bInsideFrustum;
        if(bInsideFrustum)
        {
          selected_items.Add(InstanceList[i]);
          sel_count++;
        }
        else
        {
        }
      }
      //Trace.WriteLine("---------------------------------");
      //Trace.WriteLine(string.Format("selected:  {0}/{1}", sel_count, this.Count));      
      //Trace.WriteLine(string.Format("near   Nx={0:N3}   Ny={1:N3}   Nz={2:N3}   D={3:N3}", fplanes[0].A, fplanes[0].B, fplanes[0].C, fplanes[0].D));
      //Trace.WriteLine(string.Format("far    Nx={0:N3}   Ny={1:N3}   Nz={2:N3}   D={3:N3}", fplanes[1].A, fplanes[1].B, fplanes[1].C, fplanes[1].D));
      //Trace.WriteLine(string.Format("right  Nx={0:N3}   Ny={1:N3}   Nz={2:N3}   D={3:N3}", fplanes[2].A, fplanes[2].B, fplanes[2].C, fplanes[2].D));
      //Trace.WriteLine(string.Format("left   Nx={0:N3}   Ny={1:N3}   Nz={2:N3}   D={3:N3}", fplanes[3].A, fplanes[3].B, fplanes[3].C, fplanes[3].D));
      //Trace.WriteLine(string.Format("top    Nx={0:N3}   Ny={1:N3}   Nz={2:N3}   D={3:N3}", fplanes[4].A, fplanes[4].B, fplanes[4].C, fplanes[4].D));
      //Trace.WriteLine(string.Format("Bottom Nx={0:N3}   Ny={1:N3}   Nz={2:N3}   D={3:N3}", fplanes[5].A, fplanes[5].B, fplanes[5].C, fplanes[5].D));
      return(selected_items);
    }
    public void Render()
    {
      //Draw selection box outline
      if((multiSelectMode == MultiSelectMode.FindEnd)&&(MdxRender.Dev != null))
      {
        Line ln = new Line(MdxRender.Dev);
        Vector2[] box = new Vector2[5];
        box[0] = new Vector2(selectionBoxStartX,selectionBoxStartY);
        box[1] = new Vector2(selectionBoxEndX,selectionBoxStartY);
        box[2] = new Vector2(selectionBoxEndX,selectionBoxEndY);
        box[3] = new Vector2(selectionBoxStartX,selectionBoxEndY);
        box[4] = new Vector2(selectionBoxStartX,selectionBoxStartY);

        ln.Begin();
        ln.Draw(box, Color.Yellow);
        ln.End();
      }
    }
  }
  
  // Note from Mono:
  // Alot of this needs some cleaning up/minor re-design I think.
  // I went though and fixed a few things, but it needs more attention.
  
  public class Instance3DCollection : CollectionBase
  {
    private Hashtable table = new Hashtable();
    private Instance3D activeSelection = null;
    private Instance3D debugObject = null;
    private Vector3 lastSelectionPosition = new Vector3();
    private SelectBox2D multiSelectBox = new SelectBox2D();
    private ArrayList selectionList = new ArrayList();
    int[] distanceSortArray;

    public void DistanceSort()
    {
      InnerList.Sort(new Instance3DDistanceComparer());
    }

    /// <summary>
    /// When an object is being edited, we want to lock the camera attitude controls.
    /// If there is no active selection, or if the active selection is an Idle or
    /// Unselected state, then Camera Lock isn't necessary.
    /// </summary>
    public bool CameraLockRequested
    {
      get
      {
        bool bLock = false;

        if(activeSelection != null)
        {
          EditMode em = activeSelection.EditMode;

          if((em != EditMode.IdleTranslate)&&
            (em != EditMode.Selected)&&
            (em != EditMode.IdleRotate)&&
            (em != EditMode.NotSelected))
          {
            bLock = true;
          }
        }
        return(bLock);
      }
    }
    public bool ObjectSelected
    {
      get{return(activeSelection != null);}
    }
    public Vector3 SelectionPosition
    {
      get
      {
        Vector3 pos = new Vector3();
        if(activeSelection != null)
          activeSelection.GetTranslation(out pos);

        return(pos);
      }
    }
    public Attitude SelectionRotation
    {
      get
      {
        Attitude rot = new Attitude();
        if(activeSelection != null)
          activeSelection.GetRotation(out rot.Roll, out rot.Pitch, out rot.Yaw);

        return(rot);
      }
    }

    public void AddSelection(Instance3D item)
    {
      if(activeSelection != null)
        activeSelection.EditMode = EditMode.NotSelected;

      activeSelection = item;
      activeSelection.EditMode = EditMode.Selected;
    }
    public void InitDebug()
    {
      Model3D cube = new Model3D("default cube");
      cube.LoadDirectionModel();
      debugObject = new Instance3D(cube);
      //debugObject.SetTranslation(0,0,5);
      //debugObject.SetRotation((float)Math.PI/4, (float)Math.PI/4, (float)Math.PI/4);
    }

    public void Render()
    {
      // View Frustum Culling
      //MdxRender.Camera.CalculateViewFrustum();

      // Just for fun, I tried to implement frustum culling based on some code and articles
      // that I found.  Long story short - it doesn't work ;p
      // Maybe it's on the right track and would be a good base to work off of?
      //      Instance3DCollection culledObjects = new Instance3DCollection();
      //      foreach (Instance3D instance in this.InnerList)
      //      {
      //        // Looks like billboards don't have a bounding box.
      //        if (instance.Model.m_BoundingBox == null) continue;
      //
      //        Matrix viewMatrix = MdxRender.Camera.GetViewMatrix();
      //        Vector3 min = new Vector3(instance.Model.m_BoundingBox.min[0],
      //          instance.Model.m_BoundingBox.min[1],
      //          instance.Model.m_BoundingBox.min[2]);
      //        min = Vector3.TransformCoordinate(min, MdxRender.Dev.Transform.View);
      //
      //        Vector3 max = new Vector3(instance.Model.m_BoundingBox.max[0],
      //          instance.Model.m_BoundingBox.max[1],
      //          instance.Model.m_BoundingBox.max[2]);
      //          max = Vector3.TransformCoordinate(max, MdxRender.Dev.Transform.View);
      //
      //        if (MdxRender.Camera.CullAABB(min, max) > 0)
      //          culledObjects.Add(instance);
      //      }

      // Implemented a more .NET approach to distance sorting.
      // Resulted in a 6fps gain on my system (AthlonXP 3000+, Radeon 9800 Pro)
      Instance3DCollection culledObjects = this;
      culledObjects.DistanceSort();

      for(int i=culledObjects.Count-1; i>=0; i--)
      {
        culledObjects[i].Render();
      }

      //for(int i=0; i<Count; i++)
//      for(int i=Count-1; i>=0; i--)
//      {
//        temp = this[distanceSortArray[i]];
//        temp.Render();
//      }

      multiSelectBox.Render();

      //m_DebugObject1.Render();
    }

    public void UpdateCulling()
    {
      //iterate through collection and set frustum culled flags
    }

    public class Instance3DDistanceComparer : IComparer
    {
      public int Compare(object x, object y)
      {
        Instance3D instanceX = (x as Instance3D);
        Instance3D instanceY = (y as Instance3D);

        // NOTE: This is kind of a bad design to explicitly access the static
        // camera object here.  Not sure if passing the camera in the constructor
        // would be any better though.
        float distanceX = MdxRender.Camera.GetObjectDistance(
          instanceX.Translation.X, instanceX.Translation.Y, instanceX.Translation.Z);
        float distanceY = MdxRender.Camera.GetObjectDistance(
          instanceY.Translation.X, instanceY.Translation.Y, instanceY.Translation.Z);

        if (distanceX < distanceY) return -1;
        if (distanceX > distanceY) return 1;
        return 0;
      }
    }
    public void MouseDown(int MouseX, int MouseY)
    {
      //mouse down is only important if there is an active selection in an edit mode
      if(activeSelection != null)
      {
        Vector3 PickRayDirection = new Vector3();
        Vector3 PickRayOrigin = new Vector3();
        MdxRender.CalculatePickRayWorld(MouseX, MouseY, out PickRayDirection, out PickRayOrigin);

        if(activeSelection != null)
          activeSelection.MouseDown(PickRayOrigin, PickRayDirection);
      }
    }
    public void MouseUp(int MouseX, int MouseY)
    {
      Vector3 PickRayDirection = new Vector3();
      Vector3 PickRayOrigin = new Vector3();
      MdxRender.CalculatePickRayWorld(MouseX, MouseY, out PickRayDirection, out PickRayOrigin);

      #region 2D Selection Box
      //see if we need to do a 2D select box
      multiSelectBox.MouseUp(MouseX, MouseY);
      if(multiSelectBox.Mode != MultiSelectMode.Disabled)
      {
        if(multiSelectBox.Mode != MultiSelectMode.Disabled)
        {
          ArrayList tmp = multiSelectBox.UpdateFrustumSelection(this);
          bool bFoundDuplicate = false;
          Instance3D inst, tmp_inst;
          //add 2d select box selection list to master selection list
          for(int s=0; s<tmp.Count; s++)
          {
            tmp_inst = (Instance3D)tmp[s];
            //check for duplication
            for(int k=0; k<selectionList.Count; k++)
            {
              inst = (Instance3D)selectionList[k];
              if(tmp_inst == inst)
              {
                bFoundDuplicate = true;
                break;
              }
            }

            if(bFoundDuplicate == false)
              selectionList.Add(tmp[s]);
          }
        }
      }
      #endregion

      //debug code
      if(activeSelection == null)
      {
        foreach(Instance3D instance in this)
        {
          if(instance.MouseUp(PickRayOrigin, PickRayDirection, false) == true)
          {
            activeSelection = instance;
          }
        }

        if((selectionList.Count != 0)&&(multiSelectBox.Mode == MultiSelectMode.Disabled))
        {
          foreach(Instance3D item in this)
            item.Selected = false;

          selectionList.Clear();
        }
        //if(m_DebugObject1.MouseUp(PickRayOrigin, PickRayDirection, false))
        //  m_ActiveSelection = m_DebugObject1;
      }
      else
      {
        activeSelection.MakeEditInactive();
        if(activeSelection.MouseUp(PickRayOrigin, PickRayDirection, true) == false)
        {
          if(activeSelection.EditMode == EditMode.NotSelected)
            activeSelection = null;
        }
      }

      UpdateSelectionLeader();
    }
    public void MouseMove(int MouseX, int MouseY)
    {
      if(activeSelection != null)
      {
        Vector3 PickRayDirection = new Vector3();
        Vector3 PickRayOrigin = new Vector3();
        MdxRender.CalculatePickRayWorld(MouseX, MouseY, out PickRayDirection, out PickRayOrigin);

        if(activeSelection != null)
          activeSelection.Hover(PickRayOrigin, PickRayDirection);
      }

      if(multiSelectBox.Mode == MultiSelectMode.FindEnd)
      {
        multiSelectBox.MouseMove(MouseX, MouseY);
        multiSelectBox.UpdateFrustumSelection(this);
        UpdateSelectionLeader();
      }
    }
    public void UpdateSelectionLeader()
    {
      bool bFoundLeader = false;

      for(int i=0; i<this.Count; i++)
      {
        this[i].SelectionLeader = false;

        if((this[i].Selected)&&(bFoundLeader == false))
        {
          this[i].SelectionLeader = true;
          bFoundLeader = true;
        }
      }
    }
    public Instance3D[] GetObjectList(ObjectType type)
    {
      // NOTE: (From Mono)
      // Why not just create an Instance3DCollection here and add the items to it?
      // It would most likely be faster than having to go through the collection twice.
      Instance3D[] list = null;
      int type_count = 0;
      for(int i=0; i<this.Count; i++)
        if(this[i].ObjectType == type)
          type_count++;

      list = new Instance3D[type_count];

      type_count = 0;
      for(int i=0; i<this.Count; i++)
      {
        if(this[i].ObjectType == type)
          list[type_count++] = this[i];
      }
      return(list);
    }
    public void ProcessSelectionEdit(int MouseX, int MouseY)
    {
      //Trace.WriteLine("edit active = " + MdxRender.Input.EditActive.ToString());
      //get selected instance
      if((activeSelection != null)&&(MdxRender.Input.EditActive))
      {
        Vector3 PickRayDirection = new Vector3();
        Vector3 PickRayOrigin = new Vector3();
        MouseX = MdxRender.Input.MouseX;
        MouseY = MdxRender.Input.MouseY;
        MdxRender.CalculatePickRayWorld(MouseX, MouseY, out PickRayDirection, out PickRayOrigin);

        //use the z-plane of most recent object selected
        float z_plane = activeSelection.ZPlane;


        //calculate mouse-movement of selected object
        Vector3 move_vector = new Vector3();
        Vector3 sel_pos = new Vector3();
        Vector3 obj_pos = new Vector3();

        //use last selected object's reference planes to figure out where to move the object
        activeSelection.GetTranslation(out obj_pos);


        if(MdxRender.Input.EditVerticalPlacement)
        {
          //do calculation to determine selection plane intersect point for XZ plane
          if((PickRayOrigin.Y - PickRayDirection.Y) != 0)
          {
            float u = (PickRayOrigin.Y - obj_pos.Y)/(PickRayOrigin.Y - PickRayDirection.Y);
      
            sel_pos.X = PickRayOrigin.X + u*(PickRayDirection.X - PickRayOrigin.X);
            sel_pos.Y = PickRayOrigin.Y + u*(PickRayDirection.Y - PickRayOrigin.Y);
            sel_pos.Z = PickRayOrigin.Z + u*(PickRayDirection.Z - PickRayOrigin.Z);

            move_vector.X = 0;
            move_vector.Y = 0;
            move_vector.Z = sel_pos.Z - lastSelectionPosition.Z;
          }
        }
        else //X-Y Object Movement
        {
          //do calculation to determine selection plane intersect point for XY plane
          if((PickRayOrigin.Z - PickRayDirection.Z) != 0)
          {
            float u = (PickRayOrigin.Z - obj_pos.Z)/(PickRayOrigin.Z - PickRayDirection.Z);
      
            sel_pos.X = PickRayOrigin.X + u*(PickRayDirection.X - PickRayOrigin.X);
            sel_pos.Y = PickRayOrigin.Y + u*(PickRayDirection.Y - PickRayOrigin.Y);
            sel_pos.Z = PickRayOrigin.Z + u*(PickRayDirection.Z - PickRayOrigin.Z);

            move_vector.X = sel_pos.X - lastSelectionPosition.X;
            move_vector.Y = sel_pos.Y - lastSelectionPosition.Y;
            move_vector.Z = 0;
          }
        }

        lastSelectionPosition = sel_pos;

        activeSelection.IncrementTranslation(move_vector);
      }
    }

    public void ResetSelectionOrientation()
    {
      if(activeSelection != null)
        activeSelection.SetRotation(0,0,0);
    }
    public void UpdateObjectColors()
    {
      TagBsp bsp = MdxRender.GetCurrentBsp;

      if(bsp != null)
      {
        float[] uv = new float[2];
        int lightmap_index;

        for(int i=0; i<this.Count; i++)
        {
          bsp.GetBSPIntersect(this[i].Translation, new Vector3(0,0,-1), out uv, out lightmap_index);

          Color clr = MdxRender.SM.m_TextureManager.GetLightmapTexelColor(bsp.m_LightmapIndex, lightmap_index, uv[0], uv[1]);
          this[i].ObjectColor = clr;
          Trace.WriteLine(string.Format("instance[{0}]: lma = {4} u = {1} v = {2}, color = {3}", i, uv[0], uv[1], clr.ToString(), lightmap_index));
        }
      }
      //get location from 3d
    }
    #region Mono code
    /// <summary>
    /// Adds an object to the collection.
    /// </summary>
    /// <param name="instance">The Instance3D to add to the collection.</param>
    public void Add(Instance3D instance)
    {
      InnerList.Add(instance);
      distanceSortArray = new int[this.Count];
      //table.Add(instance.Name, instance);
    }
    
    /// <summary>
    /// Removes an object from the collection.
    /// </summary>
    /// <param name="instance">The Instance3D to remove from the collection.</param>
    public void Remove(Instance3D instance)
    {
      InnerList.Remove(instance);
      distanceSortArray = new int[this.Count];
      //table.Remove(instance.Name);
    }

    /// <summary>
    /// Removes an object from the collection.
    /// </summary>
    /// <param name="name">The name of the Instance3D to remove from the collection.</param>
    public void Remove(string name)
    {
      Instance3D instance = (table[name] as Instance3D);
      distanceSortArray = new int[this.Count];
      //table.Remove(name);
      InnerList.Remove(instance);
    }

    /// <summary>
    /// Indicates if the specified object is contained in the collection.
    /// </summary>
    /// <param name="instance">The Instance3D to search for.</param>
    public bool Contains(Instance3D instance)
    {
      return InnerList.Contains(instance);
    }

    /// <summary>
    /// Retrieves an object via it's index.
    /// </summary>
    public Instance3D this[int index]
    {
      get { return (InnerList[index] as Instance3D); }
    }
    
    /// <summary>
    /// Retrieves an object via its name.
    /// </summary>
    public Instance3D this[string name]
    {
      get { return (table[name] as Instance3D); }
    }
    #endregion
  } 
}
