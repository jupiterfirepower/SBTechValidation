"# SBTechValidation" 
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
> fmap (+3) (Just 2)
Just 5
</p>
<p>
Вот что происходит за сценой, когда мы пишем fmap (+3) (Just 2):
</p>
<p align="left">
<img src="/img/functor/4.png" width="350" title="hover text">
</p>
<p>
функтор: вы применяете функцию к упакованному значению, используя fmap или <$>
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
           then Just (x `div` 2)<br>
           else Nothing<br>
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
    Nothing >>= func = Nothing<br>
    Just val >>= func  = func val<br>
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
</p>
<p>Более детально<br>
https://habr.com/ru/post/183150/<br>
http://adit.io/posts/2013-04-17-functors,_applicatives,_and_monads_in_pictures.html<br>
</p>
<p>
https://fsharpforfunandprofit.com/<br>
</p>
<p align="left">
<img src="/img/monad/5.png" title="hover text">
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

