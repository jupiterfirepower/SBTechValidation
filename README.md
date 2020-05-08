"# SBTechValidation" 
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
Example: Validation using applicative style and monadic style<br>
"applicative" vs "monadic" style<br>
applicative -> applicative functor<br>
in context validation - main idea aggregate errors(by validation rules) or construct correct object.<br>
monadic style -> mean chainable combine (main idea get only first error in chain or construct correct object.<br>
Railway Oriented Programming to describe this style of monadic error handling.<br>
</p>
<p>
Compose<br>
Next we have compose, which lets us pipe two bound functions together.<br>
<b>If the first function returns an Error, the second is never called. </b>
</p>
<p>
let compose f1 f2 =<br>
&ensp;&ensp;&ensp;&ensp;fun x -> bind f2 (f1 x)<br>
<br>
// bind operator<br>
let (>>=) a b =<br>
&ensp;&ensp;&ensp;&ensp;bind b a<br>
<br> 
// compose operator<br>
let (>=>) a b =<br>
&ensp;&ensp;&ensp;&ensp;compose a b<br>
</p>

