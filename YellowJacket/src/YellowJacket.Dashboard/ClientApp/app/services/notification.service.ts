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

import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';

import { Notification, NotificationType } from '../components/common/notification/notification.component';

@Injectable()
export class NotificationService {
    private subject = new Subject<Notification>();
    private keepAfterRouteChange = false;

    /**
     * Initialize a new instance of JobService.
     * @param {Router} router Angular router component.
     */
    constructor(private readonly router: Router) {
        router.events.subscribe(event => {
            if (event instanceof NavigationStart) {
                if (this.keepAfterRouteChange) {
                    this.keepAfterRouteChange = false;
                } else {
                    this.clear();
                }
            }
        });
    }

    /**
     * Get the notification.
     * @returns {Observable<IJob>} An Angular Observable object.
     */
    public getNotification(): Observable<any> {
        return this.subject.asObservable();
    }

    /**
     * Add a success notifiation.
     * @param {string} message The notification message. 
     * @param {boolean} keepAfterRouteChange Check if we want to keep the notification after a route change.
     */
    public addSuccess(message: string, keepAfterRouteChange = false) {
        this.addNotification(NotificationType.Success, message, keepAfterRouteChange);
    }

    /**
     * Add an error notifiation.
     * @param {string} message The notification message. 
     * @param {boolean} keepAfterRouteChange Check if we want to keep the notification after a route change.
     */
    public addError(message: string, keepAfterRouteChange = false) {
        this.addNotification(NotificationType.Error, message, keepAfterRouteChange);
    }

    /**
     * Add an info notifiation.
     * @param {string} message The notification message. 
     * @param {boolean} keepAfterRouteChange Check if we want to keep the notification after a route change.
     */
    public addInfo(message: string, keepAfterRouteChange = false) {
        this.addNotification(NotificationType.Info, message, keepAfterRouteChange);
    }

    /**
     * Add a warning notifiation.
     * @param {string} message The notification message. 
     * @param {boolean} keepAfterRouteChange Check if we want to keep the notification after a route change.
     */
    public addWarning(message: string, keepAfterRouteChange = false) {
        this.addNotification(NotificationType.Warning, message, keepAfterRouteChange);
    }

    /**
     * Clear the notification list.
     */
    public clear() {
        this.subject.next();
    }

    /**
     * Add a notification to the list.
     * @param {NotificationType} type The type of the notification.
     * @param {string} message The notification message.
     * @param {boolean} keepAfterRouteChange Check if we want to keep the notification after a route change.
     */
    private addNotification(type: NotificationType, message: string, keepAfterRouteChange = false) {
        this.keepAfterRouteChange = keepAfterRouteChange;
        this.subject.next({ type: type, message: message } as Notification);
    }
}