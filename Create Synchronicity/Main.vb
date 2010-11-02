Module ApplicationLauncher
    Friend MainFormInstance As MainForm
    Friend Translation As LanguageHandler
    Friend ProgramConfig As ConfigHandler

    Private Blocker As Threading.Mutex = Nothing

    <STAThread()> _
    Sub Main()
        Application.EnableVisualStyles()

        ' Load program configuration
        ProgramConfig = ConfigHandler.GetSingleton
        Translation = LanguageHandler.GetSingleton

        ' Start the program itself
        ConfigHandler.LogAppEvent("Program started, configuration loaded")
        CommandLine.ReadArgs(New List(Of String)(Environment.GetCommandLineArgs()))

        ' Check if multiple instances are allowed.
        If CommandLine.RunAs = CommandLine.RunMode.Scheduler Then
            If SchedulerAlreadyRunning() Then
                ConfigHandler.LogAppEvent("Scheduler already runnning from " & Application.ExecutablePath)
                Exit Sub
            End If
        End If

        ''''''''''''''''''''''''''
        ' Display help if needed '
        ''''''''''''''''''''''''''
        If CommandLine.Help Then
            Interaction.ShowMsg(String.Format("Create Synchronicity, version {1}.{0}{0}Profiles are loaded from ""{2}"".{0}{0}Available commands:{0}    /help,{0}    /scheduler,{0}    /log,{0}    [/preview] [/quiet|/silent] /run ""ProfileName1|ProfileName2|ProfileName3[|...]""{0}{0}License information: See ""Release notes.txt"".{0}{0}Help: See http://synchronicity.sourceforge.net/help.html.{0}{0}You can help this software! See http://synchronicity.sourceforge.net/contribute.html.{0}{0}Happy syncing!", Environment.NewLine, Application.ProductVersion, ProgramConfig.ConfigRootDir), "Help!")
        Else
            'Launch main form.
            MainFormInstance = New MainForm()
            Application.Run(MainFormInstance)
        End If

        'Unlike ReleaseMutex(), Close() will only release the mutex if Blocker holds it.
        'Calling ReleaseMutex() would only work if the application was not already running, ie. that this instance did create the Mutex.
        If CommandLine.RunAs = CommandLine.RunMode.Scheduler Then Blocker.Close()

        ConfigHandler.LogAppEvent("Program exited")
    End Sub

    Function SchedulerAlreadyRunning() As Boolean
        Dim MutexName As String = "[[Create Synchronicity scheduler]] " & Application.ExecutablePath.Replace("\"c, "!"c).ToLower
#If DEBUG Then
        ConfigHandler.LogAppEvent(String.Format("Trying to register mutex ""{0}""", MutexName))
#End If

        Try
            Blocker = New Threading.Mutex(False, MutexName)
        Catch Ex As Threading.AbandonedMutexException
#If DEBUG Then
            ConfigHandler.LogAppEvent("Abandoned mutex detected")
#End If
            Return False
        End Try

        Return (Not Blocker.WaitOne(0, False))
    End Function
End Module
