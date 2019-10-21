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

// MainFrm.h : interface of the CMainFrame class
//

#pragma once
#include "ChildView.h"
#include "WindowsDesktopHost.h"

class CXamlRibbonBar : public CMFCRibbonBar
{
protected:
    LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam) override;
};

class CMainFrame : public CFrameWndEx
{

public:
    CMainFrame() noexcept;
protected:
    DECLARE_DYNAMIC(CMainFrame)

    // Attributes
public:

    // Operations
public:

    // Overrides
public:
    BOOL PreCreateWindow(CREATESTRUCT& cs) override;

    // Implementation
public:
    virtual ~CMainFrame();
#ifdef _DEBUG
    virtual void AssertValid() const;
    virtual void Dump(CDumpContext& dc) const;
#endif

protected:  // control bar embedded members

    BOOL PreTranslateMessage(MSG* pMsg) override;

protected:
    afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
    afx_msg void OnSize(UINT nType, int cx, int cy);
    afx_msg void OnEnterSizeMove();
    void OnExitSizeMove();
    afx_msg void OnExitSizeMove(int cx, int cy);
    afx_msg void OnSysCommand(UINT cmdId, LPARAM lParam);
    afx_msg void OnLButtonDown(UINT, CPoint);

protected:
    LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam) override;

    DECLARE_MESSAGE_MAP()
private:
    void OnPointerReleased(winrt::Windows::Foundation::IInspectable sender, winrt::Windows::UI::Xaml::Input::PointerRoutedEventArgs eventArgs);
    void OnPointerMoved(winrt::Windows::Foundation::IInspectable sender, winrt::Windows::UI::Xaml::Input::PointerRoutedEventArgs eventArgs);
    void OnPointerPressed(winrt::Windows::Foundation::IInspectable sender, winrt::Windows::UI::Xaml::Input::PointerRoutedEventArgs eventArgs);
    void OnDoubleTapped(winrt::Windows::Foundation::IInspectable sender, winrt::Windows::UI::Xaml::Input::DoubleTappedRoutedEventArgs eventArgs);
    bool OnSysStartDrag();

private:
    CXamlRibbonBar     m_wndRibbonBar;
    CChildView    m_wndView;
    winrt::Windows::UI::Xaml::FrameworkElement m_content = nullptr;
    CWindowsDesktopHost m_xamlIsland;
};


