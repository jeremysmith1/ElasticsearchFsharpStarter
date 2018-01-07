module BackupPlan

open System
open System.IO


let readLines (filePath:string) = seq {
    use sr = new StreamReader (filePath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}

let JsonDataCollection = 
    let directory = new DirectoryInfo(@".\InternetBackup\LocalSavedData")
    directory.GetFiles()
    |> Array.map (fun x -> (readLines x.FullName) 
                        |> String.Concat)
    
    
    

