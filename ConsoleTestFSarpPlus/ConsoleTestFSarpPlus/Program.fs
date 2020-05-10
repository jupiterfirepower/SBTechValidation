open FSharpPlus

[<EntryPoint>]
let main _ =
    //Most containers are functors
    let timer = new System.Diagnostics.Stopwatch()
    timer.Start()
    let _ = List.map (fun x -> string (x + 10)) [ 1..100_000 ]
    printfn "List.map Elapsed Time: %A" timer.Elapsed

    let timer = new System.Diagnostics.Stopwatch()
    timer.Start()
    let _ = map (fun x -> string (x + 10)) [ 1..1000_000 ]
    printfn "FSharpPlus map Elapsed Time: %A" timer.Elapsed

    let timer = new System.Diagnostics.Stopwatch()
    timer.Start()
    let _ = Seq.map (fun x -> string (x + 10)) [ 1..1000_000 ]
    printfn "Seq.map Elapsed Time: %A" timer.Elapsed

    0 // return an integer exit code

