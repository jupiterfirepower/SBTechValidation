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







