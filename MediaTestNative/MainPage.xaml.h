//
// MainPage.xaml.h
// Declaration of the MainPage class.
//

#pragma once

#include "MainPage.g.h"

namespace MediaTestNative
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public ref class MainPage sealed
	{
	public:
		MainPage();

    private:
        void InitializeMPE();
        void InitializeME();
        void OnCreateMPE(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e);
        void OnCreateME(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e);
        void OnClear(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e);
        void OnCrash(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e);

        void OnTapped(Platform::Object^ sender, Windows::UI::Xaml::Input::TappedRoutedEventArgs^ e);
        void OnDoubleTapped(Platform::Object^ sender, Windows::UI::Xaml::Input::DoubleTappedRoutedEventArgs^ e);
        void OnPointerPressed(Platform::Object^ sender, Windows::UI::Xaml::Input::PointerRoutedEventArgs^ e);
    protected:

        void OnKeyUp(Windows::UI::Core::CoreWindow ^sender, Windows::UI::Core::KeyEventArgs ^args);
        void OnKeyDown(Windows::UI::Core::CoreWindow ^sender, Windows::UI::Core::KeyEventArgs ^args);
        void OnXamlKeyUp(Platform::Object ^sender, Windows::UI::Xaml::Input::KeyRoutedEventArgs ^e);
    };
}
