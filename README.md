"# Applicative style and monadic style Validation" 
<p align="left">
  <p>fmap (+3) (Just 2)</p>
  <img src="/img/functor/1.png" width="350" title="hover text">
</p>
<p align="left">
Так что такое функтор на самом деле?
Функтор — это класс типов. Вот его определение:
</p>
<p align="left">
<img src="/img/functor/2.png" width="350" title="hover text">
</p>
<p>
Функтором является любой тип данных, для которого определено, как к нему применяется fmap. А вот как fmap работает:
</p>
<p align="left">
<img src="/img/functor/3.png" width="350" title="hover text">
</p>
<p>
Так что мы можем делать так: <br>
> fmap (+3) (Just 2)<br>
Just 5
</p>
<p>
Вот что происходит за сценой, когда мы пишем fmap (+3) (Just 2):
</p>
<p align="left">
<img src="/img/functor/4.png" width="350" title="hover text">
</p>
<p>
<b>функтор</b>: вы применяете функцию к упакованному значению, используя fmap или <$>
</p>
<p>
Аппликативные функторы
<p>
Оператор <*>, который знает, как применить функцию, упакованную в контекст, к значению, упакованному в контекст:
<p>
<p align="left">
<img src="/img/applicativefunctor/1.png" width="350" title="hover text">
</p>
<p>
Just (+3) <*> Just 2 == Just 5
</p>
<p>
Аппликативные функторы: <br>
> (+) <$> (Just 5)<br>
Just (+5)<br>
> Just (+5) <*> (Just 3)<br>
Just 8<br>
</p>
<p>
Аппликативные функторы применяют упакованную функцию к упакованному же значению:
<p>
<p align="left">
<img src="/img/applicativefunctor/2.png" width="350" title="hover text">
</p>
<p>
<b>аппликативный функтор</b>: вы применяете упакованную функцию к упакованному значению, используя <*> или liftA
</p>
<p>
<b>Монады</b>
</p>
<p>
Монады применяют функцию, которая возвращает упакованное значение, к упакованному значению.<br> 
У монад есть функция >>= (произносится «связывание» (bind)), позволяющая делать это.<br>
Рассмотрим такой пример: наш старый добрый Maybe — это монада:<br>
</p>
<p align="left">
<img src="/img/monad/1.png" width="350" title="hover text">
</p>
<p>
Пусть half — функция, которая работает только с чётными числами: <br>
half x = if even x <br>
&nbsp;&nbsp;&nbsp;&nbsp;then Just (x `div` 2)<br>
&nbsp;&nbsp;&nbsp;&nbsp;else Nothing<br>
</p>
<p>
Вот как она работает:<br>
> Just 3 >>= half<br>
Nothing<br>
> Just 4 >>= half<br>
Just 2<br>
> Nothing >>= half<br>
Nothing<br>
</p>
<p>
Monad — ещё один класс типов. Вот его частичное определение:
</p>
<p align="left">
<img src="/img/monad/2.png" width="350" title="hover text">
</p>
<p>
Maybe — это монада:<br>
instance Monad Maybe where<br>
&nbsp;&nbsp;&nbsp;Nothing >>= func = Nothing<br>
&nbsp;&nbsp;&nbsp;Just val >>= func  = func val<br>
</p>
<p>
Можно так же связать цепочку из вызовов:<br>
> Just 20 >>= half >>= half >>= half<br>
Nothing <br>
20->10->5->Nothing
</p>
<p>
<b>монада</b>: вы применяете функцию, возвращающую упакованное значение, к упакованному значению, используя >>= или liftM
</p>
<p>
Заключение(in Haskell терминологии)<br>
Функтор — это тип данных, реализуемый с помощью класса типов Functor<br>
Аппликативный функтор — это тип данных, реализуемый с помощью класса типов Applicative<br>
Монада — это тип данных, реализуемый с помощью класса типов Monad<br>
Maybe реализуется с помощью всех трёх классов типов, поэтому является функтором, аппликативным функтором и монадой одновременно<br>
<br>
Функтор: вы применяете функцию к упакованному значению, используя fmap или <$><br>
Аппликативный функтор: вы применяете упакованную функцию к упакованному значению, используя <*> или liftA<br>
Монада: вы применяете функцию, возвращающую упакованное значение, к упакованному значению, используя >>= или liftM<br>
</p>
<p>Более детально<br>
https://habr.com/ru/post/183150/<br>
http://adit.io/posts/2013-04-17-functors,_applicatives,_and_monads_in_pictures.html<br>
</p>
<p>In Haskell, an applicative functor is defined like this:
</p>
<p>
class Functor f => Applicative f where<br>
&nbsp;&nbsp;pure  :: a -> f a<br>
&nbsp;&nbsp;(<*>) :: f (a -> b) -> f a -> f b<br>
</p>
<p>
This is a simplification; there's more to the Applicative typeclass than this, but this should highlight the essence.<br>
What it says is that an applicative functor must already be a Functor.<br> 
It could be any sort of Functor, like [] (linked list), Maybe, Either, and so on.<br>
Since Functor is an abstraction, it's called f.<br>
<br>
The definition furthermore says that in order for a functor to be applicative, two functions must exist: pure and <*> ('apply').<br>
pure is easy to understand. It simply 'elevates' a 'normal' value to a functor value.<br> 
For example, you can elevate the number 42 to a list value by putting it in a list with a single element: [42].<br> 
Or you can elevate "foo" to Maybe by containing it in the Just case: Just "foo".<br> 
That is, literally, what pure does for [] (list) and Maybe.<br>

The <*> operator applies an 'elevated' function to an 'elevated' value.<br> 
When f is [], this literally means that you have a list of functions that you have to apply to a list of values.<br> 
Perhaps you can already see what I meant by combinations of things.<br>
</p>
<p>
An F# perspective #
Applicative functors aren't explicitly modelled in F#, but they're easy enough to add if you need them.<br> 
F# doesn't have typeclasses, so implementing applicative functors tend to be more on a case-by-case basis.<br>
<br>
If you need list to be applicative, pure should have the type 'a -> 'a list, and <*> should have the type ('a -> 'b) list -> 'a list -> 'b list.<br> 
At this point, you already run into the problem that pure is a reserved keyword in F#, so you'll have to find another name, or simply ignore that function.<br>
<br>
If you need option to be applicative, <*> should have the type ('a -> 'b) option -> 'a option -> 'b option. <br>
Now you run into your second problem, because which function is <*>? The one for list, or the one for option? 
It can't be both, so you'll have to resort to all sorts of hygiene to prevent these two versions of the same operator from clashing.<br> 
This somewhat limits its usefulness.<br>
</p>
<p>
A C# perspective<br>
Applicative functors push the limits of what you can express in C#, but the equivalent to <*> would be a method with this signature:<br>
public static Functor<TResult> Apply<T, TResult>(<br>
&nbsp;&nbsp;&nbsp;&nbsp;this Functor<Func<T, TResult>> selector,<br>
&nbsp;&nbsp;&nbsp;&nbsp;Functor<T> source)<br>
Here, the class Functor<T> is a place-holder for a proper functor class. A concrete example could be for IEnumerable<T>:<br>
public static IEnumerable<TResult> Apply<T, TResult>(<br>
&nbsp;&nbsp;&nbsp;&nbsp;this IEnumerable<Func<T, TResult>> selectors,<br>
&nbsp;&nbsp;&nbsp;&nbsp;IEnumerable<T> source)<br>
As you can see, here you somehow have to figure out how to combine a sequence of functions with a sequence of values.<br>
</p>
<p>
person <!> name <*> age<br>
An example is a message stream processing system where events/messages are like Name and Age, which are both required to construct a Person value.<br> 
A message processor maintains state, where data pieces collected so far are represented as Option<Name> and Option<Age> and so on.<br> 
Then there is a simple function person : Name -> Age -> Person, expressing the fact that all those pieces are required to make a Person.<br>
The order in which events arrive is not defined, so the processor must wait until a Person can be made. This is elegantly expressed by<br>
<br>
person <!> name <*> age<br>
which produces a Some if all data has been collected.<br> 
</p>
<p>
https://fsharpforfunandprofit.com/<br>
</p>
<p align="left">
<img src="/img/monad/55.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/72.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/73.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/6.jpg" title="hover text">
</p>
<p align="left">
<img src="/img/monad/7.jpg" width="500" title="hover text">
</p>
<p align="left">
<img src="/img/monad/8.png" width="500" title="hover text">
</p>
<p align="left">
<img src="/img/monad/4.png" title="hover text">
</p>
<p>
<b>Example: Validation using applicative style and monadic style</b><br>
https://fsharpforfunandprofit.com/posts/elevated-world-3/#validation<br>
https://swlaschin.gitbooks.io/fsharpforfunandprofit/content/posts/elevated-world-3.html<br>
</p>
<p align="left">
<img src="/img/monad/33.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/14s.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/15s.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/16s.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/17s.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/18s.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/19s.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/20s.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/21s.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/22s.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/23s.png" title="hover text">
</p>
<p>
<a href="https://github.com/jupiterfirepower/SBTechValidation/tree/master/BadExampleConsoleFSharpFunForProfit">Bad Example FSharpFunForProfit Source code</a>
</p>
<p align="left">
<p>Американцы удивляются, как "русские"(россияне) взламывают их системы. :)</p>
<p>As joke: Don't be stupid be happy.<br>
Such as: Don't be crazy(psycho). (типа психанул, написал кучу кода ...)<br></p>
<p align="left">                            
<img src="/img/monad/74.png" width="150" title="crazy p">
</p>
<p>Output:<br></p>
<p align="left">                            
<img src="/img/monad/75.png" width="150" title="crazy p">
</p>
<p>по сути тоже самое что application crash.</p>
<p align="left">                            
<img src="/img/monad/crazyp.jpg" width="150" title="crazy p">
<img src="/img/monad/crazyp22.png" width="150" title="crazy p">
</p>
<p align="left">
<img src="/img/monad/43.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/44.png" title="hover text">
</p>
<p> <b>FP style programming подразумевает нечто совершенно другое - компилируется работает(концепция)<br> 
и нет возможности ошибок в последующем коде (противоречащим определенным правилам предметной области)</b><br>
не имеюются ввиду ошибки на уровне логики программиста - то есть написал не ту логику, не тот результат-><br>
компилируется работает-> не тот результат(программист заложил неправильную логику, ошибка в логике программиста)-> но работает<br>
</p>
<p>
<b>Some normal/correct/adequate(theory and practice together) Example</b>: Validation using applicative style <br>
</p>
<p align="left">
<img src="/img/monad/12.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/13.png" title="hover text">
</p>
<p>
<a href="https://github.com/jupiterfirepower/SBTechValidation/tree/master/ConsoleApplicativeValidation">Source code</a>
</p>
<p align="left">
<img src="/img/monad/cybs2.jpg" title="hover text">
</p>
<p> <b>or the same as previous<b></p>
<p align="left">
<img src="/img/monad/24.png" title="hover text">
</p>
<p>
<a href="https://github.com/jupiterfirepower/SBTechValidation/tree/master/ConsoleApplicativeValidationRebConstructor">Source code</a>
</p>
<p><b>Chessie package very simple example</b></p>
<p align="left">
<img src="/img/monad/25.png" title="hover text">
</p>
<p>
<a href="https://github.com/jupiterfirepower/SBTechValidation/tree/master/ConsoleChessieValidation">Source code</a>
</p>
<p>Useful link - https://fsprojects.github.io/FSharpPlus/applicative-functors.html</p>
<p align="left">
<img src="/img/monad/35.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/36.png" title="hover text">
</p>
<p>
<a href="https://github.com/jupiterfirepower/SBTechValidation/tree/master/ConsoleTestFSarpPlus">Source code</a>
</p>
<p align="left">
<img src="/img/monad/37.png" title="hover text">
</p>
<p><b>Seq.map (fun x -> string (x + 10)) [ 1..100_000 ] быстрее за FSharpPlus map (fun x -> string (x + 10)) [ 1..100_000 ] в 6.666 раз, почти в 7 раз (в ~4-7 раз медленнее за Seq.map)</b></p>
<p align="left">
<p>
<b>Иногда можна получить не совсем тот результат, на который расчитывали используя библиотеку FSharpPlus.</b><br>
</p>
<img src="/img/monad/32.png" title="hover text">
</p>
<p>
<a href="https://github.com/jupiterfirepower/SBTechValidation/tree/master/ConsoleApplicativeValidationWithMixedStyle">Source code</a>
</p>
<p>
From FSharpPlus lib source code<br>
</p>
</p>
<img src="/img/monad/52.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/31.png" title="hover text">
</p>
<p>
<b>(Комбинация операторов в библиотеке FSharpPlus работает как -> monadic style)</b><br>
<br>
let badPerson = makePerson "" "alfredgmail.com" -5<br>
printfn "Person - %A" badPerson<br> 
Person - Error [NameBetween1And50] -> ожидалось список из трех ошибок.<br>
</p>
<p><b>Полезным, в таких случаях будут примеры из первоисточника(так сказать) - Haskell</b></p>
<p align="left">
<img src="/img/monad/26.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/27.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/28.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/29.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/30.png" title="hover text">
</p>
<p><b>Используйте - но не в таком стиле</b></p>
<p align="left">
<img src="/img/monad/34.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/41.png" title="hover text">
</p>
<p>
<a href="https://github.com/jupiterfirepower/SBTechValidation/tree/master/ConsoleAccidentalFishLibValidation">Source code</a>
</p>
<p align="left">
<img src="/img/monad/42.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/49.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/47.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/48.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/45.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/50.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/46.png" title="hover text">
</p>
<p>
<a href="https://github.com/jupiterfirepower/SBTechValidation/tree/master/ConsoleOptionApplicative">Source code</a>
</p>
<p align="left">
<img src="/img/monad/51.png" title="hover text">
</p>
<p>
<a href="https://github.com/jupiterfirepower/SBTechValidation/tree/master/ConsoleFSharpPlusApplicative">Source code</a>
</p>
<p align="left">
<img src="/img/monad/59.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/56.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/57.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/58.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/61.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/62.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/63.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/64.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/65.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/66.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/67.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/68.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/69.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/70.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/71.png" title="hover text">
</p>






