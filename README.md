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






