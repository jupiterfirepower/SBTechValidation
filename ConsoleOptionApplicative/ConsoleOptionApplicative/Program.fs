let (<!>) = Option.map   // Option is a functor

let lift x = Some x      // not used, but still conceptually important

let (<*>) fOpt xOpt =    // apply applicative
    match fOpt, xOpt with
        | Some f, Some x -> Some (f x)
        | _ -> None

[<EntryPoint>]
let main _ =
    Some ((*) 3 4) |> printfn "%A"
    //(Some 3) * (Some 4) no overload * operator can't do it
    Option.map (fun x -> (*) x) (Some 3) <*> Some 4 |> printfn "%A" 
    //Option.map (fun x -> (*) x) (Some 3) -> Some ((*) 3) <*> Some 4 -> Some ((*) 3 4) -> Some 12
    Some ((*) 4) <*> (Some 3) |> printfn "%A"
    Some (*) <*> Some 3 <*> Some 4 |> printfn "%A"

    let mult = (*)
    mult <!> Some 3 <*> Some 4 |> printfn "%A"
    //mult <!> Some 3 -> Some ((*) 3) - partial apply no second argument for mult - (*) Some ((*) 3 4)-> Some 12
    //Some ((*) 3) <*> Some 4 -> Some f, Some x -> Some (f x), f = (*) 3 x = 4 -> Some (f x)->Some ((*) 3 4)
    // fmap (+3) (Just 2) -> Just 5
    Option.map (fun x -> ((+) 3) x) (Some 3) |> printfn "Option - %A" 
    // map (*) (Some 3) -> Some ((*) 3) - partial apply
    // Option.map (fun x -> (*) x) (Some 3) -> Some ((*) 3) - partial apply
    mult <!> None   <*> Some 4 |> printfn "%A"
    mult <!> Some 3 <*> None   |> printfn "%A"

    // output:
    // Some 12
    // Some 12
    // Some 12
    // Some 12
    // Some 12
    // Some 12
    // Option - Some 6
    // <null>
    // <null>

    0 // return an integer exit code

