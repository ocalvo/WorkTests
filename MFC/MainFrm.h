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
    virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
    virtual BOOL OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo);

    // Implementation
public:
    virtual ~CMainFrame();
#ifdef _DEBUG
    virtual void AssertValid() const;
    virtual void Dump(CDumpContext& dc) const;
#endif

protected:  // control bar embedded members

protected:
    afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
    afx_msg void OnSetFocus(CWnd* pOldWnd);
    afx_msg void OnApplicationLook(UINT id);
    afx_msg void OnUpdateApplicationLook(CCmdUI* pCmdUI);
    afx_msg void OnFilePrint();
    afx_msg void OnFilePrintPreview();
    afx_msg void OnUpdateFilePrintPreview(CCmdUI* pCmdUI);
    DECLARE_MESSAGE_MAP()
private:
    CMFCRibbonBar     m_wndRibbonBar;
    CMFCRibbonApplicationButton m_MainButton;
    CMFCToolBarImages m_PanelImages;
    CMFCRibbonStatusBar  m_wndStatusBar;
    CChildView    m_wndView;

    CWindowsDesktopHost m_xamlIsland;
};


