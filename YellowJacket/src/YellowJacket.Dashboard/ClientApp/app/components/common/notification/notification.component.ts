import { Component, OnInit } from '@angular/core';

import { Notification, NotificationType } from '../../../models/notification.model';
import { NotificationService } from '../../../services/notification.service';

@Component({
    selector: 'notification-component',
    templateUrl: 'notification.component.html',
    providers: [{ provide: 'INotificationService', useClass: NotificationService }]
})
export class NotificationComponent implements OnInit {
    public notifications: Notification[] = [];

    constructor(private readonly notificationService: NotificationService) {
    }

    public ngOnInit() {
        this.notificationService.getNotification().subscribe((notification: Notification) => {
            if (!notification) {
                this.notifications = [];

                return;
            }

            this.notifications.push(notification);
        });
    }

    public removeNotification(notification: Notification) {
        this.notifications = this.notifications.filter(x => x !== notification);
    }

    public cssClass(notification: Notification) {
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