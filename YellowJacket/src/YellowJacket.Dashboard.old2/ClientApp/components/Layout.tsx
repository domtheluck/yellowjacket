/// <reference path="common/progress.tsx" />
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

import * as React from 'react';
import * as $ from 'jquery';

import { Router, Route, Link } from 'react-router-dom';

import Progress from './common/Progress';
import Navigation from './common/Navigation';
import Footer from './common/Footer';
import TopHeader from './common/TopHeader';
import { correctHeight, detectBody } from './layouts/Helpers';

export class Layout extends React.Component<any, any> {
    componentDidMount() {
        // Run correctHeight function on load and resize window event
        $(window).bind('load resize', () => {
            correctHeight();
            detectBody();
        });

        // Correct height of wrapper after metisMenu animation.
        $('.metismenu a').click(() => {
            setTimeout(() => { correctHeight(); }, 300);
        });
    };

    render() {
        //const wrapperClass = `gray-bg ${this.context.location}`;
        const wrapperClass = 'gray-bg skin-3';
        return (
            <div id="wrapper">
                <Progress/>
                <Navigation location={this.context.location}/>
                <div id="page-wrapper" className={wrapperClass}>
                    <TopHeader/>
                    {this.props.children}
                    <Footer/>
                </div>
            </div>
        );
    }


}

//import * as React from 'react';
//import { NavMenu } from './NavMenu';

//export class Layout extends React.Component<{}, {}> {
//    public render() {
//        return <div className='container-fluid'>
//            <div className='row'>
//                <div className='col-sm-3'>
//                    <NavMenu />
//                </div>
//                <div className='col-sm-9'>
//                    { this.props.children }
//                </div>
//            </div>
//        </div>;
//    }
//}
