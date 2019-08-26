//
// MainPage.xaml.cpp
// Implementation of the MainPage class.
//

#include "pch.h"
#include "MainPage.xaml.h"

using namespace AKTester;

using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

MainPage::MainPage()
{
    InitializeComponent();

    Window::Current->CoreWindow->KeyUp += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Core::CoreWindow ^, Windows::UI::Core::KeyEventArgs ^>(this, &AKTester::MainPage::OnKeyUp);

    AccessKeyManager::IsDisplayModeEnabledChanged += ref new Windows::Foundation::TypedEventHandler<Platform::Object ^, Platform::Object ^>(this, &AKTester::MainPage::OnIsActiveChanged);

    bt01->AccessKey = "1";
    bt01->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown2);
    bt01->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden2);
    bt01->AccessKeyInvoked += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^>(this, &AKTester::MainPage::OnMainAccessKeyInvoked);
    bt01->IsAccessKeyScope = true;

    bt02->AccessKey = "2";
    bt02->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown2);
    bt02->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden2);
    bt02->AccessKeyInvoked += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^>(this, &AKTester::MainPage::OnMainAccessKeyInvoked);
    bt02->IsAccessKeyScope = true;

    bt01It01->AccessKey = "A";
    bt01It01->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown);
    bt01It01->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden);
    bt01It01->AccessKeyInvoked += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyInvoked);

    bt01It02->AccessKey = "B";
    bt01It02->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown);
    bt01It02->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden);
    bt01It02->AccessKeyInvoked += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyInvoked);

    bt01It03->AccessKey = "C";
    bt01It03->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown);
    bt01It03->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden);

    bt01It0301->AccessKey = "1";
    bt01It0301->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown);
    bt01It0301->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden);
    //bt01It0301->AccessKeyScopeOwner = bt01It03;
    bt01It0301->AccessKeyInvoked += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyInvoked);

    bt01It0302->AccessKey = "2";
    bt01It0302->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown);
    bt01It0302->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden);
    //bt01It0302->AccessKeyScopeOwner = bt01It03;
    bt01It0302->AccessKeyInvoked += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyInvoked);

    bt02It01->AccessKey = "A";
    bt02It01->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown);
    bt02It01->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden);
    bt02It01->AccessKeyInvoked += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyInvoked);

    bt02It02->AccessKey = "B";
    bt02It02->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown);
    bt02It02->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden);
    bt02It02->AccessKeyInvoked += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyInvoked);

    bt02It03->AccessKey = "C";
    bt02It03->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown);
    bt02It03->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden);

    bt02It0301->AccessKey = "1";
    bt02It0301->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown);
    bt02It0301->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden);
    //bt02It0301->AccessKeyScopeOwner = bt02It03;
    bt02It0301->AccessKeyInvoked += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyInvoked);

    bt02It0302->AccessKey = "2";
    bt02It0302->AccessKeyDisplayRequested += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyShown);
    bt02It0302->AccessKeyDisplayDismissed += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyHidden);
    //bt02It0302->AccessKeyScopeOwner = bt02It03;
    bt02It0302->AccessKeyInvoked += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Xaml::UIElement ^, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^>(this, &AKTester::MainPage::OnAccessKeyInvoked);
}

void MainPage::OnKeyUp(Windows::UI::Core::CoreWindow ^ sender, Windows::UI::Core::KeyEventArgs ^args)
{
}

int isActiveCount = 0;

void MainPage::OnIsActiveChanged(Platform::Object ^, Platform::Object ^)
{
    txt->Text = "IsActive " + AccessKeyManager::IsDisplayModeEnabled.ToString() + "(" + (isActiveCount++).ToString() + ")";
    if (AccessKeyManager::IsDisplayModeEnabled)
    {
        //cmdBar->Focus(Windows::UI::Xaml::FocusState::Keyboard);
    }
}

void MainPage::OnAccessKeyShown(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^args)
{
    int count = 0;
    if (dynamic_cast<FrameworkElement^>(sender)->Tag==nullptr)
    {
        dynamic_cast<FrameworkElement^>(sender)->Tag = count;
    }
    else
    {
        count = (int)(dynamic_cast<FrameworkElement^>(sender)->Tag);
    }
    count++;
    dynamic_cast<FrameworkElement^>(sender)->Tag = count;
    auto t = "bt" + "(" + sender->AccessKey + ","+ count + ")";

    auto bt = dynamic_cast<MenuFlyoutItem^>(sender);
    if (bt)
    {
        bt->Text = t;
    }

    auto subItem = dynamic_cast<MenuFlyoutSubItem^>(sender);
    if (subItem)
    {
        subItem->Text = t;
    }

    //auto bt = static_cast<Button^>(sender);
    //bt->Content = "bt" + "(" + sender->AccessKey + ")";
}

void MainPage::OnAccessKeyHidden(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^args)
{
    auto t = "bt" + sender->AccessKey;

    auto bt = dynamic_cast<MenuFlyoutItem^>(sender);
    if (bt)
    {
        bt->Text = t;
    }

    auto subItem = dynamic_cast<MenuFlyoutSubItem^>(sender);
    if (subItem)
    {
        subItem->Text = t;
    }

    //auto bt = static_cast<Button^>(sender);
    //bt->Content = "bt" + sender->AccessKey;
}

void MainPage::OnAccessKeyShown2(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyDisplayRequestedEventArgs ^args)
{
    auto bt = dynamic_cast<AppBarButton^>(sender);
    bt->Content = "app" + "(" + sender->AccessKey + ")";
    //auto bt = static_cast<PivotItem^>(sender);
    //bt->Header = "app" + "(" + sender->AccessKey + ")";
}

void MainPage::OnAccessKeyHidden2(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyDisplayDismissedEventArgs ^args)
{
    auto bt = dynamic_cast<AppBarButton^>(sender);
    bt->Content = "app" + sender->AccessKey;
    //auto bt = static_cast<PivotItem^>(sender);
    //bt->Header = "app" + sender->AccessKey;
}

void MainPage::OnMainAccessKeyInvoked(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^args)
{
    txt2->Text = "AK invoked " + sender->AccessKey;
}

void MainPage::OnAccessKeyInvoked(Windows::UI::Xaml::UIElement ^sender, Windows::UI::Xaml::Input::AccessKeyInvokedEventArgs ^args)
{
    //args->Handled = true;
    AccessKeyManager::ExitDisplayMode();
    txt2->Text = "AK invoked " + sender->AccessKey;
}


void AKTester::MainPage::autoBox_QuerySubmitted(Windows::UI::Xaml::Controls::AutoSuggestBox^ sender, Windows::UI::Xaml::Controls::AutoSuggestBoxQuerySubmittedEventArgs^ args)
{

}
