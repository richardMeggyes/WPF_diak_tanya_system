   M S B A M L   `   `   `  ����  DiakTanyaSystem    DiakTanyaSystem.Settings   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � 	Resources  5	         loginMouseEnter  registerMouseEnter  loginMouseLeave  registerMouseLeave��%t   &	        &	      &	 &     &	 9     �� 5
   
   6   �� 5   
   6   �� 5   
   6   �� 5   
   6   5      .��5      � -   5         �Name$ mainMenuLayoutRoot��5      
���   �.+�5   
   �� -   5         ��Name$ ScrollViewer��5      $��0,0,0,30q�6-   .��5      �� .+�5      �� .+�5      �� .+�5      �� $
��Left=�6    $
��Wrap=�6;   $��Beállítások��6O   $	��Top=�6b   
�������$��20(�6�   �� 5      $
��Left=�6$   $��0,5,0,0q�6?   $
��Wrap=�6P   $��Adatbázis kezelés��6d   $
��Bold"�6}   $��14(�6�   $	��Top=�6�   
�������6�   �� 5      -   5         ��Name$ tableInfoTB��5       $
��Left=�65   $��0,5,0,0q�6P   $
��Wrap=�6a   $	��Top=�6u   
�������6�   � 5      -   5         �Name$ RadioButtonWrapPanel��5   $   5      � 5      -   5        $ textBoxWrapPanel��5   $   5      5       �� 5"      $��0,5,0,5q�6   .+�5#      � $��0,0,0,5q�6$   .+�5$      �� -   5         ��Name$ newPizza��5$   !   $��Új sor felvétele��63   $	��110��6N   $
��Left=�6Z   6u   �� 5%      -   5        $ 	editPizza��5%   !   $%��Kiválasztott sor szerkesztése��64   $	��170��6\   $
��Left=�6h   6�   �� 5&      -   5        $ deletePizzak��5&   %   $!��Kiválasztott sor törlése��6;   $	��140��6^   $
��Left=�6j   6�   5'      �� 5(      -	   5         ��Name$ itemTextBox��5(   "   $��23��6P    �� TextWrapping$
 Wrap=�6\   �� 5)      -
   5        $ 
PizzakList��5)   $   $
��Bold"�68   $��15(�6J   $��0,10,0,0q�6X   $
��Wrap=�6j   $��Adattábla tartalma��6~   
�������6�   �� 5*      -   5         ��Name$ pizzaListBox��5*      $	��100��6U   
���   �
�������6u   5+      � 5,      .+�5-      �� -   5        $ 
readPizzak��5-   !   $��Lista frissítés��65   $	��140��6O   $
��Left=�6[   6v   5.      � 5/      $��0,20,0,0q�6   .+�50      �� $*��$Felület méret (alap=1.5) Érték: ��6$   $
��Bold"�6L   $��15(�6^   
�������6l   �� 51      
�������$��Center=�6m   ��6$   �� 	 �� ElementName$	 UIScaleSlider��
 �� Path$
 Value�6m   52      �� 53      .+�54      �� -   5         ��Name$ UIScaleSlider��54   !    � Minimum$	 0.8Q�68    � Maximum$ 4Q�6F    �� IsSnapToTickEnabled .  �� TickFrequency$	 0.1Q�56   !   65   57      �� 58      $��0,20,0,0q�6!   .+�59      �� -   5         ��Name$ autoMAximize��59   #   $5��/Ablak utomatikus maximalizálása indításnál��69   $
��Bold"�6n   $��15(�6�   
�������6�   �� 5:      -   5        $ noUnderDefPrice��5:   #   $<��6A Pizza ára ne menjen minuszolásnál az alapár alá��6<   $
��Bold"�6x   $��15(�6�   
�������6�   �� 5;      -   5        $ noMinusPrice��5;   #   $1��+A pizza ára ne csökkenjen minuszolásnál��69   $
��Bold"�6k   $��15(�6}   
�������6�   5<      5?      5A      5B      �� 5C   
   $��Bottom=�6   .+�5D      �� -   5        $ statusTextBlock��5D      $
��Left=�61   $
��Wrap=�6L   $�� ��6`   $	��Top=�6h   
�������$��14(�6�   5F      5H      5I      