   M S B A M L   `   `   `  ����%  !Kiinteistöpalvelufirman_sovellus1    +Kiinteistöpalvelufirman_sovellus.Orderlist   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	Tilaukset��5   	   $
��Auto��6   $
��Auto��6)    � SizeToContent$ WidthAndHeight=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$  Images/order_list-icon.png��5   	   ��5      �� 5	       �� ImageSource$$ Images\sovellus_taustakuva.jpg��6    � 	Resources 5         NoChromeButton �� 
TargetType �� Property �� Value	 �� 
TargetType
 �Name J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName��%  &	        �� 5      " ��6#   .�5      ��  � ��
 ���� 6%   �� 5       � ��$ 1q�6*   �� 5       � ��# � ��6%   �� 5       � ��$ Center=�65   �� 5       � ��$ Center=�63   �� 5       � ��$ 1q�6"   �� 5       � �� 5   
   �� 5      "	 ��6   .R�5      � $
 Chrome��6   #��z��6#    . .+�5      �� #��z��6!   #��z��6d   ��. # z 6�   #��z��6�   5       5      P� 5       � {� .  . �5      ��  � �� �#ADADAD6/   �� 5      $ Chrome��6,    �@J�Opacity$	 0.5Q�6@   5      5      5!   	   5"      5#      .��5$      � -   5        $
 
LayoutRoot��5$   	   .+�5%      �� -   5        #���   5%      $	��Top=�6I   $
��left=�6a   $��	22,15,0,0q�6|   $��42��6�    � ToolTip$< 6Klikkaamalla kuvaan pääset muokkamaan omia tietojasi��6�    � Cursor$
 Hand��6�   $��34��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   .��5&      �� -   5         ��Name$ 	user_icon��5&      $��Images/gender-man.png��6!   5'      �� 5(      -   5         ��Name$ 	user_name��5(      $��29��6#   $	��Top=�6/   $��13(�6H   $��ExtraCondensed&�6V   $��	10,57,0,0q�6s   $
��Left=�6�   $	��290��6�   xxxx6�   �� 5)      -   5        #���   5)      $	��Top=�66   $��Right=�6N   $��974,18,10,454q�6�   $ -0.095,1.286&�6�   $ Kirjaudu ulos��6�   $
 Hand��6�   .��5*      �� $��Images/logout-icon.png��6   $��27��6/   $��43��6;   $ 	1.6,0.333&�6F   5+      �� 5,      $��23��6   $	��Top=�6   $��Right=�64   $��13(�6P   $��0,52,0,0q�6^   $ 	1.4,0.391&�6p   $��80��6�   Kirjaudu Ulos6�   �� 5.      -   5        $��textBlockHeading��5.   K   $��24��6   $
��Left=�6   $��
10,136,0,0q�67   $��	Tilaukset��6c   $	��Top=�6t   $	��141��6�   $��20(�6�   $��ExtraCondensed&�6�   $
��Bold"�6�   �� 5/      -   5         ��Name$ Submit��5/   #   $��Uusi tilaus��6   $��Bottom=�63   $��Center=�6O   $
 Hand��6�   $��444,0,485,10q�6�   $��35��6�   $	��100��6�   &   System.Windows.Controls.DataGrid  51      -   5          Name$ 	dg_Orders��51      $��10,177,10,70q�6"   $��Stretch=�68   $��Stretch=�6T      AutoGenerateColumns .  "   VerticalScrollBarVisibility$
 Auto=�52         CanUserDeleteRows .    CanUserAddRows .  ��6   ��    Columns 53      0  *System.Windows.Controls.DataGridTextColumn  54   
   ,  &System.Windows.Controls.DataGridColumn   
Visibility$ Hidden=�6;      Header$ ID��6O   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *	tilaus_id+6O     55   
      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6I   $ Tilauspäivämäärä��6V       
IsReadOnly  .  6   �� *	tilauspvm+! �� Mode$! TwoWay=�6p     56   
     . $
 Auto 6Z   $ Palvelu��6g    6   �� *
palvelut+$! TwoWay=�6g     57   
     . $
 Auto 6]   $ Käyntiosoite��6j    6   �� *kayntiosoite+$! TwoWay=�6j     58   
     . $
 Auto 6^   $ Asunnontyyppi��6k    6   �� *asunnontyyppi+$! TwoWay=�6k     59   
     . $
 Auto 6_   $ Laskutusosoite��6l    6   �� *laskutusosoite+$! TwoWay=�6l     5:   
   $
 Auto 6L     . $ Hyväksymispvm��6k    6   �� *hyvaksymispvm+$! TwoWay=�6k     5;   
   $
 Auto 6I     . $ Aloituspäivämäärä��6h    6   �� *
aloituspvm+$! TwoWay=�6h     5<   
   $
 Auto 6M     . $  Valmistumispäivämäärä��6l    6   �� *Valmistumispvm+$! TwoWay=�6l     5=   
   $
 Auto 6I     . $ Tuntimäärä��6h    6   �� *
tuntimaara+$! TwoWay=�6h     5>   
   $
 Auto 6G     . $ Selostus��6f    6   �� *
selostus+$! TwoWay=�6f     5?   
   -	   5        	txtStatus��$
 Auto 5?   V     . $ Käsittelyn tila��6u    6   �� *tila+$! TwoWay=�6u   4  .System.Windows.Controls.DataGridTemplateColumn  5@   
   $ Poista tilaus��6!     . $
 Auto 6J   "   CellTemplate" 5A      �� 5B      .R�5C      �� $ Delete_button��69   -
   5        $��Poista��5C      $��Delete��6(   6P   5D      5F      5H      5I      5J      