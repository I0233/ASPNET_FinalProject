   M S B A M L   `   `   `  ����%  !Kiinteistöpalvelufirman_sovellus2    ,Kiinteistöpalvelufirman_sovellus.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Pääikkuna��5   	   $
��Auto��6   $
��Auto��6(    � SizeToContent$ WidthAndHeight=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ Images/login-icon.png��5   	   ��5      �� 5	       �� ImageSource$$ Images/sovellus_taustakuva.jpg��6   .��5      � $	��696��6	   $	��401��6   .+�5      �� -   5        $��LoginHeading��5   K   $��23��6   $
��Left=�6   $��
152,10,0,0q�67   $1��+Tervetuloa huoltofirmanpalvelu sovellukseen��6_   $	��Top=�6�   $��20(�6�   $��ExtraCondensed&�6�   $��Arial)�6�   �� 5      -   5        $��
textBlock1��5   K   $��23��6   $
��Left=�6   $��
66,120,0,0q�67   $��Sähköposti��6]   $	��Top=�6o   $��67��6�   �� 5      -   5        $��
textBlock2��5   K   $��23��6   $
��Left=�6   $��
66,168,0,0q�67   $��Salasana��6]   $	��Top=�6m   $��77��6�   �� 5      -   5        $��txtUserName��5   J   $��23��6   $
��Left=�6   $��138,120,0,0q�65    � ToolTip$: 4Syötä sähköpostisi muodossa esim. todo@xxxx.com ��6]   $	��Top=�6�   $	��247��6�   G� 5      -   5        $��txtPassword��5   L   $��23��6   $
��Left=�6   $��138,168,0,0q�67   $	��Top=�6_   $	��247��6w   �� 5      -   5        $��btnLogin��5   `   $��Kirjaudu sisään��6   $��23��6%   $
��Left=�61   $��
66,225,0,0q�6L   $	��Top=�6p   $	��104��6�    � Cursor$
 Hand��6�   �� 5      -   5         ��Name$ errormessage��5   5   $��61��6   $
��Left=�6   $
��Wrap=�6L   $	��Top=�6`   $	��620��6x   $��
66,275,0,0q�6�    J� OpacityMask
 �<�����	#FFE5572C	 �� TextAlignment$	 Justify=�6�   �� 5      -   5        $ textBlockHeading��5   H   $��50��6   $
��Left=�6   $��	10,0,0,10q�65   $��Bottom=�6b   $��12(�6}   $��Italic$�6�   
 �� Padding$
 5q�6�   �� 5      $'�!Sinulla ei ole tunnuksia vielä? ��6
   �� 64   -	   5        $��14(�5   U   $��Normal$�6c   .�6w   �� $�Reksiteröidy��6{   6�   5      5      5      