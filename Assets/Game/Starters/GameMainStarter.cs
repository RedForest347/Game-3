using RangerV;


public class GameMainStarter : Starter
{
    public override void StarterSetup()
    {
        
        GlobalSystemStorage.Add<CursorProc>();
        
        GlobalSystemStorage.Add<CorutineManager>();
        
        GlobalSystemStorage.Add<FirstProc>();
       
        GlobalSystemStorage.Add<PanelProc>();
        GlobalSystemStorage.Add<CameraProc>();

        /*GlobalSystemStorage.Add<CameraProc>();
        GlobalSystemStorage.Add<MoveProc>();
        GlobalSystemStorage.Add<PressButtonProc>();
        GlobalSystemStorage.Add<CloseDoorProc>();
        GlobalSystemStorage.Add<DoorOCStateProc>();
        GlobalSystemStorage.Add<PedestalProc>();
        GlobalSystemStorage.Add<ShowTextProc>();*/

        //часть процессингов запускается в PanelProc
    }
}
