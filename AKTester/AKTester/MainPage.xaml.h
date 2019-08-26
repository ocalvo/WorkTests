//
// MainPage.xaml.h
// Declaration of the MainPage class.
//

#pragma once

#include "MainPage.g.h"

namespace AKTester
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public ref class MainPage sealed
	{
	public:
		MainPage();

        void OnIsActiveChanged(Platform::Object ^ sender, Platform::Object ^ args);

        void OnAccessKeyShown(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^args);
        void OnAccessKeyShown2(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^args);

        void OnAccessKeyHidden(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^args);
        void OnAccessKeyHidden2(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^args);

        void OnMainAccessKeyInvoked(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^args);
        void OnAccessKeyInvoked(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^args);

        void OnKeyUp(Windows::UI::Core::CoreWindow ^ sender, Windows::UI::Core::KeyEventArgs ^args);
    private:
        void autoBox_QuerySubmitted(Windows::UI::Xaml::Controls::AutoSuggestBox^ sender, Windows::UI::Xaml::Controls::AutoSuggestBoxQuerySubmittedEventArgs^ args);
    };
}
