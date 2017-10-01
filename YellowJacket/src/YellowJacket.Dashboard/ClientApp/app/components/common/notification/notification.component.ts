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
    constructor(private readonly notificationService: NotificationService) {}

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