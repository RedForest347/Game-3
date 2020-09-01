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
        GlobalSystemStorage.Add<StepProc>();
        GlobalSystemStorage.Add<DoorSoundProc>();
        GlobalSystemStorage.Add<PressMidlleRoomButtonProc>();
        GlobalSystemStorage.Add<MidlleRoomDoorsProc>();

        //часть процессингов запускается в PanelProc
    }
}
