"# Applicative style and monadic style Validation" 
<p align="left">
  <p>fmap (+3) (Just 2)</p>
  <img src="/img/functor/1.png" width="350" title="hover text">
</p>
<p align="left">
��� ��� ����� ������� �� ����� ����?
������� � ��� ����� �����. ��� ��� �����������:
</p>
<p align="left">
<img src="/img/functor/2.png" width="350" title="hover text">
</p>
<p>
��������� �������� ����� ��� ������, ��� �������� ����������, ��� � ���� ����������� fmap. � ��� ��� fmap ��������:
</p>
<p align="left">
<img src="/img/functor/3.png" width="350" title="hover text">
</p>
<p>
��� ��� �� ����� ������ ���: <br>
> fmap (+3) (Just 2)
Just 5
</p>
<p>
��� ��� ���������� �� ������, ����� �� ����� fmap (+3) (Just 2):
</p>
<p align="left">
<img src="/img/functor/4.png" width="350" title="hover text">
</p>
<p>
�������: �� ���������� ������� � ������������ ��������, ��������� fmap ��� <$>
</p>
<p>
������������� ��������
<p>
�������� <*>, ������� �����, ��� ��������� �������, ����������� � ��������, � ��������, ������������ � ��������:
<p>
<p align="left">
<img src="/img/applicativefunctor/1.png" width="350" title="hover text">
</p>
<p>
Just (+3) <*> Just 2 == Just 5
</p>
<p>
������������� ��������: <br>
> (+) <$> (Just 5)<br>
Just (+5)<br>
> Just (+5) <*> (Just 3)<br>
Just 8<br>
</p>
<p>
������������� �������� ��������� ����������� ������� � ������������ �� ��������:
<p>
<p align="left">
<img src="/img/applicativefunctor/2.png" width="350" title="hover text">
</p>
<p>
<b>������������� �������</b>: �� ���������� ����������� ������� � ������������ ��������, ��������� <*> ��� liftA
</p>
<p>
<b>������</b>
</p>
<p>
������ ��������� �������, ������� ���������� ����������� ��������, � ������������ ��������.<br> 
� ����� ���� ������� >>= (������������ ����������� (bind)), ����������� ������ ���.<br>
���������� ����� ������: ��� ������ ������ Maybe � ��� ������:<br>
</p>
<p align="left">
<img src="/img/monad/1.png" width="350" title="hover text">
</p>
<p>
����� half � �������, ������� �������� ������ � ������� �������: <br>
half x = if even x <br>
           then Just (x `div` 2)<br>
           else Nothing<br>
</p>
<p>
��� ��� ��� ��������:<br>
> Just 3 >>= half<br>
Nothing<br>
> Just 4 >>= half<br>
Just 2<br>
> Nothing >>= half<br>
Nothing<br>
</p>
<p>
Monad � ��� ���� ����� �����. ��� ��� ��������� �����������:
</p>
<p align="left">
<img src="/img/monad/2.png" width="350" title="hover text">
</p>
<p>
Maybe � ��� ������:<br>
instance Monad Maybe where<br>
    Nothing >>= func = Nothing<br>
    Just val >>= func  = func val<br>
</p>
<p>
����� ��� �� ������� ������� �� �������:<br>
> Just 20 >>= half >>= half >>= half<br>
Nothing <br>
20->10->5->Nothing
</p>
<p>
<b>������</b>: �� ���������� �������, ������������ ����������� ��������, � ������������ ��������, ��������� >>= ��� liftM
</p>
<p>
����������(in Haskell ������������)<br>
������� � ��� ��� ������, ����������� � ������� ������ ����� Functor<br>
������������� ������� � ��� ��� ������, ����������� � ������� ������ ����� Applicative<br>
������ � ��� ��� ������, ����������� � ������� ������ ����� Monad<br>
Maybe ����������� � ������� ���� ��� ������� �����, ������� �������� ���������, ������������� ��������� � ������� ������������<br>
</p>
<p>����� ��������<br>
https://habr.com/ru/post/183150/<br>
http://adit.io/posts/2013-04-17-functors,_applicatives,_and_monads_in_pictures.html<br>
</p>
<p>
https://fsharpforfunandprofit.com/<br>
</p>
<p align="left">
<img src="/img/monad/55.png" title="hover text">
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
<p align="left">
<p>���������� ����������, ��� �������(��������) ���������� �� �������. :)</p>
<p>As joke: Don't be stupid be happy.<br>
Such as: Don't be crazy(psycho).
</p>
<p align="left">
<img src="/img/monad/crazyp.jpg" title="crazy p">
</p>
<p align="left">
<img src="/img/monad/38.png" title="hover text">
</p>
<p> <b>FP style programming ������������� ����� ���������� ������ - ������������� ��������(���������)<br> 
� ��� ����������� ������ � ����������� ���� (�������������� ������������ �������� ���������� �������)</b><br>
�� �������� ����� ������ �� ������ ������ ������������ - �� ���� ������� �� �� ������, �� ��� ���������<br>
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
<p> Like as:<br>
</p>
<p align="left">
<img src="/img/monad/cybs1.jpg" title="hover text">
</p>
<p> <b>or the same as previous</b></p>
<p align="left">
<img src="/img/monad/24.png" title="hover text">
</p>
<p>Chessie package very simple example</p>
<p align="left">
<img src="/img/monad/25.png" title="hover text">
</p>
<p>
<b>������ ����� �������� �� ������ ��� ���������, �� ������� ����������� ��������� ����������. <br>
(�������� � ���������� FSharpPlus ����� ������ ��������(������) � �������� ��� monadic style</b>
</p>
<p>Useful link - https://fsprojects.github.io/FSharpPlus/applicative-functors.html</p>
<p align="left">
<img src="/img/monad/35.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/36.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/37.png" title="hover text">
</p>
<p><b>Seq.map (fun x -> string (x + 10)) [ 1..100_000 ] ������� �� FSharpPlus map (fun x -> string (x + 10)) [ 1..100_000 ] � 6.666 ���, ����� � 7 ���  </b></p>
<p align="left">
<img src="/img/monad/32.png" title="hover text">
</p>
<p align="left">
<img src="/img/monad/31.png" title="hover text">
</p>
<p><b>��������, � ����� ������� ����� ������� �� ��������������(��� �������) - Haskell</b></p>
<p align="left">
<img src="/img/monad/26.png" title="hover text">
</p>
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
<p><b>����������� - �� �� � ����� �����</b></p>
<p align="left">
<img src="/img/monad/34.png" title="hover text">
</p>









