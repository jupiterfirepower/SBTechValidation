open System
open FSharpPlus

type Error = 
    | NameBetween1And50
    | EmailMustContainAtChar
    | AgeBetween0and120

module ConstrainedTypes =
    type Name = private { unName : String } 
    with static member create s = 
          // Smart constructors
          let mkName s = 
            let l = length s
            in if (l >= 1 && l <= 50)
              then Ok <| { unName = s }
              else Error [ NameBetween1And50 ]
          {| Result = mkName s; TimeStamp = DateTime.Now |}

    type Email = private { unEmail : String } 
    with static member create s = 
                       let mkEmail s = 
                          if String.contains '@' s
                            then Ok <| { unEmail = s}
                            else Error [ EmailMustContainAtChar ]
                       {| Result = mkEmail s; TimeStamp = DateTime.Now |}

    type Age = private { unAge : int }
    with static member create i =
                           let mkAge a = 
                              if (a >= 0 && a <= 120)
                                then Ok <| { unAge = i}
                                else Error [ AgeBetween0and120 ]
                           {| Result = mkAge i; TimeStamp = DateTime.Now |}

    type Person = private { name : Name
                            email : Email
                            age : Age } 
    with static member create name email age = { name = name; email = email; age = age }

open ConstrainedTypes

let makePerson pName pEmail pAge =
  Person.create
  <!> (Name.create pName).Result
  <*> (Email.create pEmail).Result
  <*> (Age.create pAge).Result

// Example
type Person = { name: string; age: int } 
with static member create n a = { name = n; age = a }

[<EntryPoint>]
let main _ =
    let person1 = Person.create <!> tryHead ["gus"] <*> tryParse "42"
    printfn "Person - %A" person1
    let person2 = Person.create <!> tryHead ["gus"] <*> tryParse "fourty two"
    printfn "Person - %A" person2
    let person3 = Person.create <!> tryHead ["gus"] <*> (tryHead ["42"] >>= tryParse)
    printfn "Person - %A" person3

    // let name = { Name.unName = "" } // The union cases or fields of the type 'Name' 
    // are not accessible from this code location  -> 
    // not compiled can't create incorrect data(incorrect domain model state by validation rules)
    let person = makePerson "Alfred" "alfred@gmail.com" 35
    printfn "Person - %A" person
    let badPerson = makePerson "" "alfredgmail.com" -5
    printfn "Person - %A" badPerson
    0 // return an integer exit code
