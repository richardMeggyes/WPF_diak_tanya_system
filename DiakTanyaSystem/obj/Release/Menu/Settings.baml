   M S B A M L   `   `   `  ����   DiakTanyaSystem    DiakTanyaSystem.Settings   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � 	Resources     loginMouseEnter  registerMouseEnter  loginMouseLeave  registerMouseLeave��%<   &	        &	      &	 
     &	      �� �� �� �� .��� -    �Name$ mainMenuLayoutRoot��
���   �.+��� -    ��Name$ ScrollViewer��$��0,0,0,30q�.���� .+��� .+��� .+��� $
��Left=�$
��Wrap=�$��Beállítások��$	��Top=�
�������$��20(��� $
��Left=�$��0,5,0,0q�$
��Wrap=�$��Adatbázis kezelés��$
��Bold"�$��14(�$	��Top=�
��������� -    ��Name$ tableInfoTB��$
��Left=�$��0,5,0,0q�$
��Wrap=�$	��Top=�
�������� -    �Name$ RadioButtonWrapPanel��� -   $ textBoxWrapPanel���� $��0,5,0,5q�.+�� $��0,0,0,5q�.+��� -    ��Name$ newPizza��$��Új sor felvétele��$	��110��$
��Left=��� -   $ 	editPizza��$%��Kiválasztott sor szerkesztése��$	��170��$
��Left=��� -   $ deletePizzak��$!��Kiválasztott sor törlése��$	��140��$
��Left=��� -	    ��Name$ itemTextBox��$��23�� �� TextWrapping$
 Wrap=��� -
   $ 
PizzakList��$
��Bold"�$��15(�$��0,10,0,0q�$
��Wrap=�$��Adattábla tartalma��
��������� -    ��Name$ pizzaListBox��$	��100��
���   �
�������� .+��� -   $ 
readPizzak��$��Lista frissítés��$	��140��$
��Left=�� $��0,20,0,0q�.+��� $*��$Felület méret (alap=1.5) Érték: ��$
��Bold"�$��15(�
��������� 
�������$��Center=����� 	 �� ElementName$	 UIScaleSlider��
 �� Path$
 Value��� .+��� -    ��Name$ UIScaleSlider�� � Minimum$	 0.8Q� � Maximum$ 4Q� �� IsSnapToTickEnabled .  �� TickFrequency$	 0.1Q��� $��0,20,0,0q�.+��� -    ��Name$ autoMAximize��$5��/Ablak utomatikus maximalizálása indításnál��$
��Bold"�$��15(�
��������� -   $ noUnderDefPrice��$<��6A Pizza ára ne menjen minuszolásnál az alapár alá��$
��Bold"�$��15(�
��������� -   $ noMinusPrice��$1��+A pizza ára ne csökkenjen minuszolásnál��$
��Bold"�$��15(�
��������� $��Bottom=�.+��� -   $ statusTextBlock��$
��Left=�$
��Wrap=�$�� ��$	��Top=�
�������$��14(�