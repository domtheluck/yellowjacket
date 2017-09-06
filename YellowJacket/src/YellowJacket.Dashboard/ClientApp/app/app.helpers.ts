/*
 * Inspinia js helpers:
 *
 * correctHeight() - fix the height of main wrapper
 * detectBody() - detect windows size
 * smoothlyMenu() - add smooth fade in/out on navigation show/ide
 *
 */

declare var jQuery: any;

import * as $ from 'jquery';

export function correctHeight() {
    const pageWrapper = $('#page-wrapper');
    const navbarHeight = $('nav.navbar-default').height();
    const wrapperHeight = pageWrapper.height();
    const windowHeight = $(window).height();

    if (navbarHeight && wrapperHeight && windowHeight) {
        if (navbarHeight > wrapperHeight) {
            pageWrapper.css('min-height', navbarHeight + 'px');
        }

        if (navbarHeight <= wrapperHeight) {
            if (navbarHeight < windowHeight) {
                pageWrapper.css('min-height', jQuery(window).height() + 'px');
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
    if (!jQuery('body').hasClass('mini-navbar') || jQuery('body').hasClass('body-small')) {
        // Hide menu in order to smoothly turn on when maximize menu
        jQuery('#side-menu').hide();
        // For smoothly turn on menu
        setTimeout(
            () => {
                jQuery('#side-menu').fadeIn(400);
            }, 200);
    } else if (jQuery('body').hasClass('fixed-sidebar')) {
        jQuery('#side-menu').hide();
        setTimeout(
            () => {
                jQuery('#side-menu').fadeIn(400);
            }, 100);
    } else {
        // Remove all inline style from jquery fadeIn function to reset menu state
        jQuery('#side-menu').removeAttr('style');
    }
}
