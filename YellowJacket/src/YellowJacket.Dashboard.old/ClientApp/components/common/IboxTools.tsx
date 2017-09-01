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

import * as React from "react";
import * as $ from "jquery";

interface IIboxToolsProps {
    collapseEnabled: boolean;
    wrenchEnabled: boolean;
    closeEnabled: boolean;
}

class IboxTools extends React.Component<IIboxToolsProps, void> {
    collapsePanel(e) {
        e.preventDefault();
        const element = $(e.target);
        var ibox = element.closest("div.ibox");
        const button = element.closest("i");
        const content = ibox.find("div.ibox-content");
        content.slideToggle(200);
        button.toggleClass("fa-chevron-up").toggleClass("fa-chevron-down");
        ibox.toggleClass("").toggleClass("border-bottom");
        setTimeout(() => {
            ibox.resize();
            ibox.find("[id^=map-]").resize();
        }, 50);
    };

    closePanel(e) {
        e.preventDefault();
        const element = $(e.target);
        const content = element.closest("div.ibox");
        content.remove();
    };

    render() {
        let collapseButton = null;

        if (this.props.collapseEnabled) {
            collapseButton = <a className="collapse-link" onClick={this.collapsePanel}><i className="fa fa-chevron-up"></i></a>;
        }

        return (
            <div className="ibox-tools">
                {collapseButton}
                <a className="dropdown-toggle" data-toggle="dropdown" href="#">
                    <i className="fa fa-wrench"></i>
                </a>
                <ul className="dropdown-menu dropdown-user">
                    <li><a href="#">Config option 1</a>
                    </li>
                    <li><a href="#">Config option 2</a>
                    </li>
                </ul>
                <a className="close-link" onClick={this.closePanel}>
                    <i className="fa fa-times"></i>
                </a>
            </div >
        );
    }
}

export default IboxTools