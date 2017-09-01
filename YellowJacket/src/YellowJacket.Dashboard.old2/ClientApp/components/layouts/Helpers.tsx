// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

import * as $ from 'jquery';

export function correctHeight() {

    const pageWrapper = $('#page-wrapper');
    const navbarHeight = $('nav.navbar-default').height();
    const wrapperHeight = pageWrapper.height();
    const windowHeight = $(window).height();

    if (pageWrapper && navbarHeight && wrapperHeight && windowHeight) {

        if (navbarHeight > wrapperHeight) {
            pageWrapper.css('min-height', navbarHeight + 'px');
        }

        if (navbarHeight < wrapperHeight) {
            if (navbarHeight < windowHeight) {
                pageWrapper.css('min-height', $(window).height() + 'px');
            } else {
                pageWrapper.css('min-height', navbarHeight + 'px');
            }
        }

        if ($('body').hasClass('fixed-nav')) {
            if (navbarHeight > wrapperHeight) {
                pageWrapper.css('min-height', navbarHeight + 'px');
            } else {
                pageWrapper.css('min-height', windowHeight - 60 + 'px');
            }
        }
    }
}

export function detectBody() {
    const documentWidth = $(document).width();

    if (documentWidth) {
        if (documentWidth < 769) {
            $('body').addClass('body-small');
        } else {
            $('body').removeClass('body-small');
        }
    }
}

export function smoothlyMenu() {
    if (!$('body').hasClass('mini-navbar') || $('body').hasClass('body-small')) {
        // Hide menu in order to smoothly turn on when maximize menu
        $('#side-menu').hide();

        // For smoothly turn on menu
        setTimeout(
            () => {
                $('#side-menu').fadeIn(400);
            }, 200);
    } else if ($('body').hasClass('fixed-sidebar')) {
        $('#side-menu').hide();

        setTimeout(
            () => {
                $('#side-menu').fadeIn(400);
            }, 100);
    } else {
        // Remove all inline style from jquery fadeIn function to reset menu state
        $('#side-menu').removeAttr('style');
    }
}
