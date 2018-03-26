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

import { Component, OnInit } from '@angular/core';

import { NotificationService } from '../../../services/notification.service';

export class Notification {
    public type: NotificationType;
    public message: string;
}

export enum NotificationType {
    Success,
    Error,
    Info,
    Warning
}

@Component({
    selector: 'notification-component',
    templateUrl: 'notification.component.html',
    providers: [{ provide: 'INotificationService', useClass: NotificationService }]
})
export class NotificationComponent implements OnInit {
    public notifications: Notification[] = [];

    /**
     * Initialize a new instance of NotificationComponent.
     * @param {NotificationService} notificationService An instance of the notification service.
     */
    constructor(private readonly notificationService: NotificationService) { }

    /**
     * {Agular} Lifecycle hook that is called after data-bound properties of a directive are initialized.
     */
    public ngOnInit() {
        this.notificationService.getNotification().subscribe((notification: Notification) => {
            if (!notification) {
                this.notifications = [];

                return;
            }

            this.notifications.push(notification);
        });
    }

    /**
     * Remove the specified notification from the list.
     * @param {Notification} notification The notification to remove.
     */
    public removeNotification(notification: Notification) {
        this.notifications = this.notifications.filter(x => x !== notification);
    }

    /**   
     * Gets the css class according to a specific notification.
     * @param {Notification} notification
     * @returns {string} The css class.
     */
    public getCssClass(notification: Notification) {
        if (!notification) {
            return '';
        }

        switch (notification.type) {
            case NotificationType.Success:
                return 'alert alert-success';

            case NotificationType.Error:
                return 'alert alert-danger';

            case NotificationType.Info:
                return 'alert alert-info';

            case NotificationType.Warning:
                return 'alert alert-warning';
        }

        return '';
    }
}