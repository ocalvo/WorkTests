
// Tester2.h : main header file for the Tester2 application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CTester2App:
// See Tester2.cpp for the implementation of this class
//

class CTester2App : public CWinApp
{
public:
	CTester2App();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CTester2App theApp;
