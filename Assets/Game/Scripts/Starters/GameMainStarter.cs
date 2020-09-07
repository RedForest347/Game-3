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

        //GlobalSystemStorage.Get<CameraProc>().need_camera = true; // костыль

        GlobalSystemStorage.Add<StepProc>();
        GlobalSystemStorage.Add<DoorSoundProc>();
        GlobalSystemStorage.Add<PressMidlleRoomButtonProc>();
        GlobalSystemStorage.Add<MidlleRoomProc>();
        GlobalSystemStorage.Add<LastRoomCompositionProc>();
        GlobalSystemStorage.Add<AutomaticOpenDoorProc>();
        GlobalSystemStorage.Add<EndStairProc>();
        GlobalSystemStorage.Add<FallProc>();
        GlobalSystemStorage.Add<ClassicMusicProc>();



        GlobalSystemStorage.Add<EndGameProc>();

        //часть процессингов запускается в PanelProc
    }
}
