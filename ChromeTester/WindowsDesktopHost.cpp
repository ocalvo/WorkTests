// WindowsDesktopHost.cpp : implementation file
//

#include "pch.h"
#include "ChromeTester.h"
#include "WindowsDesktopHost.h"

IMPLEMENT_DYNAMIC(CWindowsDesktopHost, CBasePane)

CWindowsDesktopHost::CWindowsDesktopHost()
{
}

CWindowsDesktopHost::~CWindowsDesktopHost()
{
}

BEGIN_MESSAGE_MAP(CWindowsDesktopHost, CBasePane)
END_MESSAGE_MAP()

static WNDPROC oldProc = nullptr;

LRESULT CALLBACK HookWndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    if (message == WM_LBUTTONDOWN)
    {
        return FALSE;
    }

    SetWindowLong(hWnd, GWL_WNDPROC, (LONG)oldProc);
    const auto result = oldProc(hWnd, message, wParam, lParam);
    SetWindowLong(hWnd, GWL_WNDPROC, (LONG)HookWndProc);
    return result;
}

BOOL CWindowsDesktopHost::Create(CWnd* pParentWnd, DWORD dwStyle, DWORD dwExStyle)
{
    auto parentWnd = pParentWnd->GetSafeHwnd();
    auto native = m_xamlIsland.as<IDesktopWindowXamlSourceNative2>();
    winrt::check_hresult(native->AttachToWindow(parentWnd));
    HWND islandWnd = nullptr;
    winrt::check_hresult(native->get_WindowHandle(&islandWnd));

    const bool result = Attach(islandWnd);

    auto style = GetWindowLong(islandWnd, GWL_STYLE);
    style |= dwStyle;
    SetWindowLong(islandWnd, GWL_STYLE, style);

    const auto inputWnd = GetWindow(GW_CHILD);
    oldProc = (WNDPROC)SetWindowLong(inputWnd->GetSafeHwnd(), GWL_WNDPROC, (LONG)HookWndProc);

    return result;
}

BOOL CWindowsDesktopHost::PreTranslateMessage(MSG* pMsg)
{
    if (pMsg->message == WM_NCHITTEST)
    {
        return FALSE;
    }

    if (pMsg->message == WM_LBUTTONDOWN)
    {
        return FALSE;
    }

    auto native = m_xamlIsland.as<IDesktopWindowXamlSourceNative2>();
    BOOL result = false;
    winrt::check_hresult(native->PreTranslateMessage(pMsg, &result));
    return result;
}

winrt::Windows::UI::Xaml::UIElement CWindowsDesktopHost::Content()
{
    return m_xamlIsland.Content();
}

void CWindowsDesktopHost::Content(winrt::Windows::UI::Xaml::UIElement content)
{
    m_xamlIsland.Content(content);
}

