#pragma once

#include <windows.h>

// Copyright (c) Microsoft Corporation.  All rights reserved.

struct Resolution
{
    // Dimensions of display
    DWORD Width;
    DWORD Height;

    // Earliest app runtime version that we can expect to have been
    // tested against this resolution.
    DWORD VersionIntroduced;

    FLOAT Scale;
};

#define FWVGA_HEIGHT 854

static const Resolution KnownResolutions[] =
{
    // Width    Height  Version when introduced    Scale
    { 1440,     2560,   7,                         1.0 },
    { 1080,     1920,   6,                         1.0 },
    { 768,      1280,   0,                         1.0 }, // All apps can run on the rest of the resolutions,
    { 720,      1280,   0,                         1.0 }, // due to other fixups in the platform.
    { 540,      960,    7,                         1.0 /*FWVGA_HEIGHT/960.0*/ },
    { 480,      854,    0,                         1.0 },
    { 480,      800,    0,                         1.0 },
};

enum class KnownResolutionIndex
{
    ResolutionQuadHD,
    Resolution1080P,
    ResolutionWXGA,
    Resolution720P,
    ResolutionQHD,
    ResolutionFWVGA,
    ResolutionWVGA
};

const float AspectRatioEpsilon = 0.005f;
const float AspectRatio15x9 = 9.0f / 15.0f;
const float AspectRatio16x9 = 9.0f / 16.0f;

float GetAspectRatio(DWORD w, DWORD h)
{
    return static_cast<float>(w) / static_cast<float>(h);
}

bool IsSameAspectRatio(DWORD w, DWORD h, float aspectRatio)
{
    const float difference = GetAspectRatio(w, h) - aspectRatio;
    return difference >= -AspectRatioEpsilon && difference <= AspectRatioEpsilon;
}

HRESULT GetScreenHeightReservedForNavBar(__out DWORD* reservedHeight)
{
    // Shell_IsNavBarFrozenForProcess and Shell_GetNavigationBarMaxHeight must
    // not change for the lifetime of the process.  Since querying them involves
    // an RPC call and multiple threads in the shell, let's just query once
    // per process.
    static bool initializedStatic = false;
    static DWORD reservedHeightStatic = 0;
    HRESULT hr = S_OK;

    if (!initializedStatic)
    {
        BOOL isFrozen = FALSE;

        if (true)
        {
            if (isFrozen)
            {
            }
        }
#ifndef NAVBAR_REQUERY_FOR_TESTING
        initializedStatic = true;
#endif
    }

    *reservedHeight = reservedHeightStatic;
    return S_OK;
}


HRESULT GetEmulatedResolution(
    __in DWORD width,
    __in DWORD height,
    __in DWORD appVersion,
    __out_opt DWORD* emulatedWidth,
    __out_opt DWORD* emulatedHeight,
    __out_opt float* emulatedScaleFactor)
{
    HRESULT hr = S_OK;

    DWORD reservedForNavBar = 0;
    hr = GetScreenHeightReservedForNavBar(&reservedForNavBar);
    if (FAILED(hr))
    {
        return hr;
    }
    height -= reservedForNavBar;

    const bool fits_16_9 = GetAspectRatio(width, height) <= AspectRatio16x9 + AspectRatioEpsilon;

    DWORD resultWidth = 0;
    DWORD resultHeight = 0;

    for (DWORD i = 0; i<sizeof(KnownResolutions) / sizeof(KnownResolutions[0]); ++i)
    {
        const Resolution& res = KnownResolutions[i];

        if (res.VersionIntroduced > appVersion)
        {
            // We don't expect that this app would have been tested against
            // this resolution
            continue;
        }

        if (!fits_16_9 && !IsSameAspectRatio(res.Width, res.Height, AspectRatio15x9))
        {
            // If the size doesn't fit into 16:9, we'll need to use a 15:9
            // resolution.  Skip any resolutions with aspect ratio other
            // than 15:9
            continue;
        }

        if (fits_16_9 && !IsSameAspectRatio(res.Width, res.Height, AspectRatio16x9))
        {
            // If the size does fit into 16:9, skip any resolutions with
            // aspect ratios other than 16:9
            continue;
        }

        if (width >= res.Width && height >= res.Height)
        {
            // Find the highest resolution that's lower-res than (or same as!)
            // the actual resolution and return.  For example, on a CityMan
            // device running with frozen navbar, we'll return the highest 15:9
            // resolution, which is WXGA.
            resultWidth = res.Width * res.Scale;
            resultHeight = res.Height * res.Scale;
            break;
        }

    }

    if (resultWidth == 0 || resultHeight == 0)
    {
        return E_UNEXPECTED;
    }

    if (emulatedWidth)
    {
        *emulatedWidth = resultWidth;
    }
    if (emulatedHeight)
    {
        *emulatedHeight = resultHeight;
    }
    if (emulatedScaleFactor)
    {
        *emulatedScaleFactor = static_cast<float>(resultWidth) / 480.0f;
    }
    return S_OK;
}
