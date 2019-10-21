// This MFC Samples source code demonstrates using MFC Microsoft Office Fluent User Interface
// (the "Fluent UI") and is provided only as referential material to supplement the
// Microsoft Foundation Classes Reference and related electronic documentation
// included with the MFC C++ library software.
// License terms to copy, use or distribute the Fluent UI are available separately.
// To learn more about our Fluent UI licensing program, please visit
// https://go.microsoft.com/fwlink/?LinkId=238214.
//
// Copyright (C) Microsoft Corporation
// All rights reserved.

// MainFrm.cpp : implementation of the CMainFrame class
//

#include "pch.h"
#include "framework.h"
#include "ChromeTester.h"

#include "MainFrm.h"
#include <ShellScalingApi.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CMainFrame

IMPLEMENT_DYNAMIC(CMainFrame, CFrameWndEx)

BEGIN_MESSAGE_MAP(CMainFrame, CFrameWndEx)
    ON_WM_CREATE()
    ON_WM_SETFOCUS()
    ON_WM_SIZE()
    ON_WM_ENTERSIZEMOVE()
    ON_WM_EXITSIZEMOVE()
    ON_WM_SYSCOMMAND()
    ON_WM_LBUTTONDOWN()
END_MESSAGE_MAP()

// CMainFrame construction/destruction

CMainFrame::CMainFrame() noexcept
{
    // TODO: add member initialization code here
    theApp.m_nAppLook = theApp.GetInt(_T("ApplicationLook"), ID_VIEW_APPLOOK_OFF_2007_AQUA);
}

CMainFrame::~CMainFrame()
{
}

LRESULT CMainFrame::WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
{
    if (message == WM_POINTERDOWN)
    {
        OnSysStartDrag();
        return 0;
    }

    return __super::WindowProc(message, wParam, lParam);
}


void CMainFrame::OnSysCommand(UINT cmdId, LPARAM lParam)
{
    __super::OnSysCommand(cmdId, lParam);
}

bool CMainFrame::OnSysStartDrag()
{
    POINT point1 = {};
    ::GetCursorPos(&point1);
    const LPARAM lParam = MAKELPARAM(point1.x, point1.y);
    const auto hWnd = GetSafeHwnd();

    //MSG msg = {};
    //if (::PeekMessageW(&msg, nullptr, 0, 0, PM_REMOVE))
    //{
        //::DispatchMessageW(&msg);
    //    SendMessage(WM_MOUSELEAVE, MK_LBUTTON, lParam);
    //    SendMessage(WM_LBUTTONUP, MK_LBUTTON, lParam);
    //}

    //const auto hInputWnd = ::GetWindow(m_xamlIsland.GetSafeHwnd(), GW_CHILD);
    //::ShowWindow(hInputWnd, SW_HIDE);
    //::EnableWindow(hInputWnd, 0);

    //MSG msg = {};
    //while (PeekMessageW(&msg, nullptr, 0, 0, PM_REMOVE))
    //{
    //    ::DispatchMessageW(&msg);
    //    if (msg.message == WM_LBUTTONDOWN)
    //    {
    //        const bool posted = PostMessage(WM_SYSCOMMAND, SC_MOVE | HTCAPTION, lParam);
    //    }
    //}

    //m_xamlIsland.SetWindowPos(NULL, 100, 100, 100, 100, 0);

    //.ShowWindow(SW_HIDE);
    //const bool posted = ::PostMessage(hWnd, WM_LBUTTONDOWN, MK_LBUTTON, lParam);
    //BYTE keyboard[256] = {};
    //winrt::check_bool(GetKeyboardState(keyboard));
    //keyboard[VK_LBUTTON] = 0x80;
    //SetKeyboardState(keyboard);

    SetActiveWindow();
    //SetCapture();

    //::PostMessage(hWnd, WM_LBUTTONDOWN, MK_LBUTTON, lParam);
    const bool posted = PostMessage(WM_SYSCOMMAND, SC_MOVE | HTCAPTION, lParam);
    //const bool posted = SendMessage(WM_SYSCOMMAND, SC_MOVE | HTCAPTION, lParam);

    //const auto dispatcherQueue = winrt::Windows::System::DispatcherQueue::GetForCurrentThread();
    //const bool posted = dispatcherQueue.TryEnqueue([=]
    //    {
    //        ::PostMessage(hWnd, WM_LBUTTONDOWN, MK_LBUTTON, lParam);
    //        ::PostMessage(hWnd, WM_SYSCOMMAND, SC_MOVE | HTCAPTION, lParam);
    //    });


    //SetCapture();
    //DefWindowProc(WM_NCLBUTTONDOWN, HTCAPTION, lParam);
    //const bool posted = true;

    return posted;
}

void CMainFrame::OnEnterSizeMove()
{
    m_xamlIsland.ShowWindow(SW_SHOW);
}

BOOL CMainFrame::PreTranslateMessage(MSG* pMsg)
{
    if (pMsg->message == WM_LBUTTONDOWN)
    {
        //OnSysStartDrag();
        return __super::PreTranslateMessage(pMsg);
    }
    else if (pMsg->message == WM_POINTERDOWN)
    {
        //OnSysStartDrag();
        return __super::PreTranslateMessage(pMsg);
    }
    
    return __super::PreTranslateMessage(pMsg);
}


void CMainFrame::OnLButtonDown(UINT, CPoint)
{
    POINT point1 = {};
    ::GetCursorPos(&point1);

    const LPARAM lParam = MAKELPARAM(point1.x, point1.y);
    const auto hWnd = GetSafeHwnd();
    ::SetActiveWindow(hWnd);
    ::PostMessage(hWnd, WM_SYSCOMMAND, SC_MOVE | HTCAPTION, lParam);
}

void CMainFrame::OnPointerPressed(winrt::Windows::Foundation::IInspectable sender, winrt::Windows::UI::Xaml::Input::PointerRoutedEventArgs eventArgs)
{
    const auto ptr = eventArgs.Pointer();
    if (ptr.PointerDeviceType() == winrt::Windows::Devices::Input::PointerDeviceType::Mouse)
    {
        const auto ptrPt = eventArgs.GetCurrentPoint(m_xamlIsland.Content());
        if (ptrPt.Properties().IsLeftButtonPressed())
        {
            eventArgs.Handled(OnSysStartDrag());
        }
    }
}

void CMainFrame::OnPointerReleased(winrt::Windows::Foundation::IInspectable sender, winrt::Windows::UI::Xaml::Input::PointerRoutedEventArgs eventArgs)
{
}

void CMainFrame::OnPointerMoved(winrt::Windows::Foundation::IInspectable sender, winrt::Windows::UI::Xaml::Input::PointerRoutedEventArgs eventArgs)
{
}

void CMainFrame::OnDoubleTapped(winrt::Windows::Foundation::IInspectable sender, winrt::Windows::UI::Xaml::Input::DoubleTappedRoutedEventArgs eventArgs)
{
    POINT point1 = {};
    ::GetCursorPos(&point1);
    const LPARAM lParam = MAKELPARAM(point1.x, point1.y);
    WINDOWPLACEMENT placement = { sizeof(placement) };
    GetWindowPlacement(&placement);
    if (placement.showCmd == SW_SHOWNORMAL)
    {
        PostMessage(WM_SYSCOMMAND, SC_MAXIMIZE | HTCAPTION, lParam);
    }
    else if (placement.showCmd == SW_SHOWMAXIMIZED)
    {
        PostMessage(WM_SYSCOMMAND, SC_RESTORE | HTCAPTION, lParam);
    }
}

LRESULT CXamlRibbonBar::WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
{
    return __super::WindowProc(message, wParam, lParam);
}

void CMainFrame::OnSize(UINT nType, int cx, int cy)
{
    OnExitSizeMove(cx, cy);
}

void CMainFrame::OnExitSizeMove()
{

}

void CMainFrame::OnExitSizeMove(int cx, int cy)
{
    if (!m_content)
    {
        return;
    }

    const auto s = GetDpiForWindow(GetSafeHwnd());
    const double dpi = double(s) / double(USER_DEFAULT_SCREEN_DPI);

    TITLEBARINFO titleBarInfo = {};
    titleBarInfo.cbSize = sizeof(titleBarInfo);
    GetTitleBarInfo(&titleBarInfo);
    const auto drag = ::GetSystemMetrics(SM_CYDRAG);
    const auto dragLogical = drag / dpi;
    const auto nonClientHeight = (titleBarInfo.rcTitleBar.bottom - titleBarInfo.rcTitleBar.top) + drag;

    RECT buttonsRect = {};
    ::DwmGetWindowAttribute(GetSafeHwnd(), DWMWA_CAPTION_BUTTON_BOUNDS, &buttonsRect, sizeof(buttonsRect));
    const auto minMaxWidth = static_cast<int>(((static_cast<float>(buttonsRect.right) - static_cast<float>(buttonsRect.left)) * 1.0) - (drag * 2.0));
    const auto dragRegionHeight = minMaxWidth;

    const auto windowsWidth = cx;
    const auto windowsHeight = cy;

    m_content.Margin({ dragLogical,dragLogical,dragLogical,dragLogical });
    m_content.Height((windowsHeight / dpi) + 0.5);
    m_content.Width((windowsWidth / dpi) + 0.5);

    m_xamlIsland.SetWindowPos(CWnd::FromHandle(HWND_TOP), 0, 0, windowsWidth, windowsHeight, SWP_SHOWWINDOW);
    m_wndRibbonBar.SetWindowPos(CWnd::FromHandle(HWND_TOP), 0, 0, windowsWidth, windowsHeight, SWP_SHOWWINDOW);

    HRGN region = CreateRectRgn(0, 0, 0, 0);
    HRGN nonClientRegion = CreateRectRgn(0, drag, windowsWidth - minMaxWidth - dragRegionHeight, nonClientHeight);
    HRGN clientRegion = CreateRectRgn(0, nonClientHeight, windowsWidth, windowsHeight);
    CombineRgn(region, nonClientRegion, clientRegion, RGN_OR);
    m_xamlIsland.SetWindowRgn(region, true);
}

int CMainFrame::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
    if (CFrameWndEx::OnCreate(lpCreateStruct) == -1)
        return -1;

    m_wndRibbonBar.Create(this);
    if (0)
    {
        m_wndRibbonBar.LoadFromResource(IDR_RIBBON);
    }
    else
    {
        m_xamlIsland.Create(&m_wndRibbonBar, WS_TABSTOP | WS_VISIBLE);

        winrt::Windows::UI::Xaml::Controls::TextBlock txt1;
        txt1.Text(L"Hello from Xaml");
        txt1.HorizontalAlignment(winrt::Windows::UI::Xaml::HorizontalAlignment::Left);
        txt1.VerticalAlignment(winrt::Windows::UI::Xaml::VerticalAlignment::Top);
        winrt::Windows::UI::Xaml::Controls::Border border;
        const auto color = GetSysColor(COLOR_ACTIVECAPTION);
        winrt::Windows::UI::Color winrtColor = {};
        winrtColor.A = 0xFF;
        winrtColor.R = GetRValue(color);
        winrtColor.G = GetGValue(color);
        winrtColor.B = GetBValue(color);
        border.Background(winrt::Windows::UI::Xaml::Media::SolidColorBrush(winrtColor));
        border.Child(txt1);

        m_content = border;

        //m_content.PointerMoved({ this, &CMainFrame::OnPointerMoved });
        m_content.PointerPressed({ this, &CMainFrame::OnPointerPressed });
        m_content.DoubleTapped({ this, &CMainFrame::OnDoubleTapped });
        //m_content.PointerReleased({ this, &CMainFrame::OnPointerReleased });
        //m_content.DragEnter({ this, &CMainFrame::OnDragEnter });

        m_xamlIsland.Content(border);

        OnExitSizeMove(100,100);
    }

    return 0;
}

BOOL CMainFrame::PreCreateWindow(CREATESTRUCT& cs)
{
    if (!CFrameWndEx::PreCreateWindow(cs))
        return FALSE;
    // TODO: Modify the Window class or styles here by modifying
    //  the CREATESTRUCT cs

    cs.dwExStyle &= ~WS_EX_CLIENTEDGE;
    cs.lpszClass = AfxRegisterWndClass(0);
    return TRUE;
}

// CMainFrame diagnostics

#ifdef _DEBUG
void CMainFrame::AssertValid() const
{
    CFrameWndEx::AssertValid();
}

void CMainFrame::Dump(CDumpContext& dc) const
{
    CFrameWndEx::Dump(dc);
}
#endif //_DEBUG

