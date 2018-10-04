namespace ZacksTest

open System
open System.Threading
open System.IO
open System.Diagnostics
open System.Reflection
open System.Windows.Forms

///the program that gets launched
module MainProgram =

    while true do
    ///current command entered into the console
    let CommandRN = Console.ReadLine()

    ///command and args array
    let WholeCommandRN = CommandRN.Split(' ')
    ///command only
    let mutable CommandRNStart = WholeCommandRN.GetValue(0)
    let mutable Int1 = 1
    let mutable Feedback = "this command is broken or you forgot to set feedback to nothing"
    let CommandRNEnd = String.Join(" ", WholeCommandRN.[1..])
    
    if (CommandRNStart.ToString() = "echo") then
        Feedback <- CommandRNEnd.ToString()
    else if (CommandRNStart.ToString() = "about") then
        Feedback <- "This program is ZacksTest. It is a test console app to help me learn the F# programming language. Type \"help\" for more info."
    else if (CommandRNStart.ToString() = "exit") then
        let ThisProcess = Process.GetCurrentProcess()
        ThisProcess.Kill()
    else if (CommandRNStart.ToString() = "runfile") then
        if (File.Exists(CommandRNEnd)) then
            let ReadSomeLines = File.ReadAllLines(CommandRNEnd)
            let ThisPath = Assembly.GetExecutingAssembly().Location
            let procStartInfo = ProcessStartInfo(FileName = ThisPath, RedirectStandardInput = true, UseShellExecute = false)
            let np = new Process(StartInfo = procStartInfo)
            np.Start()
            for line in ReadSomeLines do
                np.StandardInput.WriteLine(line)
            Feedback <- ""
        else Feedback <- "Incorrect path!"
    else if (CommandRNStart.ToString() = "help") then
        Console.WriteLine("--HELP--")
        Console.WriteLine("ABOUT: Shows app info")
        Console.WriteLine("ECHO %text: Prints back %text")
        Console.WriteLine("HELP: Prints this help screen")
        Console.WriteLine("EXIT: Exits the console")
        Console.WriteLine("RUNFILE %path: Runs file at %path (including extension) in ZacksTest script")
        Feedback <- "--HELP--"
    else
        Feedback <- "Unrecognized command."
    Console.WriteLine(Feedback)