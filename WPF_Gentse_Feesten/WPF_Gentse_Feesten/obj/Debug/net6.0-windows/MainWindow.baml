   M S B A M L   `   `   `  ����>  :Gui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WPF_Gentse_FeestenWPF_Gentse_Feesten  5   	   A =Domein, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=+clr-namespace:Domein.Models;assembly=DomeinDomein.Models 5   	   #    WPF_Gentse_Feesten.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local1 clr-namespace:WPF_Gentse_Feesten     5   	   =local+clr-namespace:Domein.Models;assembly=Domein     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)    � DataContext 5
   	   ��  �� RelativeSource �� *Self+��5      �� 5   
    �� ImageSource$ /bg.jpg��6   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      .+�5   
   �� $��10q�6    �� Orientation$ 
Horizontal=�6!   .+�5      �� -   5        $��TxtZoek��5      $	��150��6%   $��Center=�64   $��10q�6V   �� 5      -   5        $��	BtnZoeken��5   3   $��Zoeken��6   $��10q�6'   6D   5      �� 5   
   -   5         ��Name$  listBoxTopLevelEvenementen��5   0   $��1��6   $��2��6   $��10q�6T    J� Opacity$ .9Q�6`   ���	#FFFFD9D96�   �� 5!   
   $��Kind Evenementen:��6   $��1��6,   $
��Left=�6<   $��10q�6W   $��Bottom=�6c   $��18(�6~   ���	#FFFFD9D9$ .9Q�6�   �� 5"   
   -   5        $ listBoxKindEvenementen��5"   /   $��1��6   $��1��6"   $��10q�6O   $ .9Q�6[   ���	#FFFFD9D96�   �� 5#   
   -   5        $# listBoxKindVanKindEvenementen��5#   /   $��1��6   $��2��6"   $��10q�6V   $ .9Q�6b   ���	#FFFFD9D96�   �� 5$   
   $��2��6   $��2��6'   $��1��68   $��10q�6E   
�������$
��Auto=�6d   .��5%   
   �� .+�5&      �� $��2q�6_   ��6   �� *SelectedEvenement.Naam+	 �� StringFormat$	 	Naam: {0}��6_   �� 5'      $
��Wrap=�6o   $��2q�6�   ��6   �� * SelectedEvenement.Beschrijving+$	 Beschrijving: {0}��6�   �� 5(      $��2q�6k   ��6   �� *SelectedEvenement.Startdatum+$	 Startdatum: {0}��6k   �� 5)      $��2q�6i   ��6   �� *SelectedEvenement.Einddatum+$	 Einddatum: {0}��6i   �� 5*      $��2q�6a   ��6   �� *SelectedEvenement.Prijs+$	 
Prijs: {0}��6a   5+      5,      �� 5-   
   $��2��6   $ 
Horizontal=�6%   $��10q�6>   .+�5.      �� -   5        $��BtnToevoegenAanPlanner��5.   @   $��Toevoegen aan planner��6   $��5q�65   6^   �� 5/      -   5        $��BtnOpenPlanner��5/   -   $��Open planner��6   $��5q�6`   50      52      54      