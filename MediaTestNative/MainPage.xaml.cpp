//
// MainPage.xaml.cpp
// Implementation of the MainPage class.
//

#include "pch.h"
#include "MainPage.xaml.h"

using namespace MediaTestNative;

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

auto posterUrlStr = L"http://1.bp.blogspot.com/-jlAmGweWyRw/T9nfCve5VAI/AAAAAAAAF2s/bYrmAuarUxU/s1600/Star-wars-wallpaper-26.jpg";
auto video1UrlStr = L"http://jolt-media/ocalvo/matroska_test_w1_1/bear.wmv";
auto video2UrlStr = L"http://jolt-media/video/ladybug.wmv";
auto video3UrlStr = L"http://jolt-media/ocalvo/matroska_test_w1_1/cctest.mp4";
auto video4UrlStr = L"http://jolt-media/video/office_intro.mp4";
auto video5UrlStr = L"http://jolt-media/video/test5.wmv";
//me->Source = ref new Windows::Foundation::Uri(L"http://jolt-media/video/bear.wmv");
//me->Source = ref new Windows::Foundation::Uri(L"http://jolt-media/ocalvo/matroska_test_w1_1/test1.mkv");
//me->Source = ref new Windows::Foundation::Uri(L"http://jolt-media/video/test5.wmv");
const wchar_t* videoUrls[] =
{
    video1UrlStr,
    video2UrlStr,
    video3UrlStr,
    video4UrlStr,
    video5UrlStr,
};
// 4 Closed captions
// 3 The Office
const int videoIndex = 4;
auto videoUrlStr = videoUrls[videoIndex];
auto audioUrlStr = L"http://www.tonycuffe.com/mp3/tail%20toddle.mp3";

MainPage::MainPage()
{
    InitializeComponent();
    auto coreWnd = Window::Current->CoreWindow;
    coreWnd->KeyUp += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Core::CoreWindow ^, Windows::UI::Core::KeyEventArgs ^>(this, &MediaTestNative::MainPage::OnKeyUp);
    coreWnd->KeyDown += ref new Windows::Foundation::TypedEventHandler<Windows::UI::Core::CoreWindow ^, Windows::UI::Core::KeyEventArgs ^>(this, &MediaTestNative::MainPage::OnKeyDown);
    this->KeyUp += ref new Windows::UI::Xaml::Input::KeyEventHandler(this, &MediaTestNative::MainPage::OnXamlKeyUp);
}

Stretch GetStretchMode(int index)
{
    switch (index)
    {
    case 0:
        return Stretch::Fill;
    case 1:
        return Stretch::None;
    case 2:
        return Stretch::Uniform;
    case 3:
        return Stretch::UniformToFill;
    }

    return Stretch::Fill;
}

double scale = 1.75;

UIElement^ m = nullptr;

void MainPage::InitializeMPE()
{
    auto mpe = ref new Windows::UI::Xaml::Controls::MediaPlayerElement();
    if (sizeChk->IsChecked->Value)
    {
        mpe->Width = 200.0 * scale;
        mpe->Height = 170.0 * scale;
    }
    MediaPanel->Children->Append(mpe);
    m = mpe;

    if (this->enableMTC->IsChecked->Value)
    {
        mpe->AreTransportControlsEnabled = true;
        mpe->AutoPlay = false;
        //mpe->TransportControls->IsFastForwardButtonVisible = true;
        //mpe->TransportControls->IsFastForwardEnabled = true;

    }
    else
    {
        mpe->AutoPlay = true;
        mpe->TransportControls = nullptr;
        mpe->AreTransportControlsEnabled = false;
    }

    mpe->Stretch = GetStretchMode(this->stretchMode->SelectedIndex);

    if (this->enableFullWindow->IsChecked->Value)
    {
        mpe->IsFullWindow = true;
    }

    auto bitmapImage = ref new  Windows::UI::Xaml::Media::Imaging::BitmapImage();
    auto urlStr = posterUrlStr;
    bitmapImage->UriSource = ref new Uri(ref new String(urlStr));
    mpe->PosterSource = bitmapImage;

    Windows::Media::Core::MediaSource^ source = nullptr;
    if (useAudio->IsChecked->Value)
    {
        source = Windows::Media::Core::MediaSource::CreateFromUri(ref new Windows::Foundation::Uri(ref new String(audioUrlStr)));
    }
    else
    {
        source = Windows::Media::Core::MediaSource::CreateFromUri(ref new Windows::Foundation::Uri(ref new String(videoUrlStr)));
    }
    auto item = ref new Windows::Media::Playback::MediaPlaybackItem(source);

    mpe->Source = item;

    Log->Text = ref new String(L"made it");

    mpe->Tapped += ref new Windows::UI::Xaml::Input::TappedEventHandler(this, &MediaTestNative::MainPage::OnTapped);
    mpe->PointerPressed += ref new Windows::UI::Xaml::Input::PointerEventHandler(this, &MediaTestNative::MainPage::OnPointerPressed);
    mpe->DoubleTapped += ref new Windows::UI::Xaml::Input::DoubleTappedEventHandler(this, &MediaTestNative::MainPage::OnDoubleTapped);
}

void MainPage::InitializeME()
{
    auto me = ref new MediaElement();
    m = me;

    if (useAudio->IsChecked->Value)
    {
        me->Source = ref new Windows::Foundation::Uri(ref new String(audioUrlStr));
    }
    else
    {
        auto uri = ref new Uri(ref new String(videoUrlStr));
        me->Source = uri;
    }

    if (this->enableMTC->IsChecked->Value)
    {
        me->AreTransportControlsEnabled = true;
        //me->TransportControls->IsPlayButtonVisible = true;
        //me->TransportControls->IsStopEnabled = true;
        me->AutoPlay = false;
    }
    if (sizeChk->IsChecked->Value)
    {
        me->Width = 200.0 * scale;
        me->Height = 170.0 * scale;
    }

    auto bitmapImage = ref new  Windows::UI::Xaml::Media::Imaging::BitmapImage();
    auto urlStr = ref new String(posterUrlStr);
    bitmapImage->UriSource = ref new Uri(urlStr);
    me->PosterSource = bitmapImage;

    if (this->enableFullWindow->IsChecked->Value)
    {
        me->IsFullWindow = true;
    }


    me->Stretch = GetStretchMode(this->stretchMode->SelectedIndex);
    MediaPanel->Children->Append(me);
    me->Tapped += ref new Windows::UI::Xaml::Input::TappedEventHandler(this, &MediaTestNative::MainPage::OnTapped);
    Log->Text = L"Legacy made it";
    me->DoubleTapped += ref new Windows::UI::Xaml::Input::DoubleTappedEventHandler(this, &MediaTestNative::MainPage::OnDoubleTapped);

    me->UpdateLayout();
    this->UpdateLayout();
}

void MediaTestNative::MainPage::OnCreateMPE(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
    try
    {
        InitializeMPE();
    }
    catch (Exception^ e)
    {
        Log->Text = e->ToString();
    }
}

void MediaTestNative::MainPage::OnCreateME(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
    try
    {
        InitializeME();
    }
    catch (Exception^ e)
    {
        Log->Text = e->ToString();
    }
}

void MediaTestNative::MainPage::OnPointerPressed(Platform::Object^ sender, Windows::UI::Xaml::Input::PointerRoutedEventArgs^ e)
{
    Log->Text = L"PonterPressed";
}

void MediaTestNative::MainPage::OnDoubleTapped(Platform::Object^ sender, Windows::UI::Xaml::Input::DoubleTappedRoutedEventArgs^ e)
{
    auto pME = dynamic_cast<MediaElement^>(sender);
    auto pMPE = dynamic_cast<MediaPlayerElement^>(sender);
    if (pME)
    {
        pME->IsFullWindow = !pME->IsFullWindow;
    }
    else if (pMPE)
    {
        pMPE->TransportControls->IsFastForwardButtonVisible = true;
        pMPE->TransportControls->IsFastForwardEnabled = true;

        //pMPE->IsFullWindow = !pMPE->IsFullWindow;
    }
    Log->Text = L"DoubleTapped";
}

int S(int l)
{
    int hr = 0;
    switch (l)
    {
    case -1:
        return 0;
    default:
        return -1;
    }

    return 0;
}

void MediaTestNative::MainPage::OnTapped(Platform::Object^ sender, Windows::UI::Xaml::Input::TappedRoutedEventArgs^ e)
{
    Log->Text = L"Tapped";
}

void MediaTestNative::MainPage::OnClear(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
    MediaPanel->Children->Clear();
    Log->Text = L"";
}

void MediaTestNative::MainPage::OnCrash(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
    auto pME = dynamic_cast<MediaElement^>(m);
    auto pMPE = dynamic_cast<MediaPlayerElement^>(m);
    if (pME)
    {
        pME->Stop();
    }
    else if (pMPE)
    {
        //auto closalble = dynamic_cast<IClosable^>(pMPE->MediaPlayer->Source);
        pMPE->SetMediaPlayer(nullptr);
    }
    Log->Text = L"";
}

void MediaTestNative::MainPage::OnKeyDown(Windows::UI::Core::CoreWindow ^sender, Windows::UI::Core::KeyEventArgs ^args)
{
    Log->Text = Log->Text + L" CoreWindow KeyDown,";
}

void MediaTestNative::MainPage::OnKeyUp(Windows::UI::Core::CoreWindow ^sender, Windows::UI::Core::KeyEventArgs ^args)
{
    Log->Text = Log->Text + L" CoreWindow KeyUp,";
}


void MediaTestNative::MainPage::OnXamlKeyUp(Platform::Object ^sender, Windows::UI::Xaml::Input::KeyRoutedEventArgs ^e)
{
    Log->Text = Log->Text + L" Xaml KeyUp,";
}
