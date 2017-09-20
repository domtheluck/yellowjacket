import { Component, OnInit, Inject } from '@angular/core';

import { Notification, NotificationType } from '../../../models/notification.model';
import { NotificationService } from '../../../services/notification.service';

@Component({
    selector: 'notification-component',
    templateUrl: 'notification.component.html',
    providers: [{ provide: 'INotificationService', useClass: NotificationService }]
})
export class NotificationComponent implements OnInit {
    //public notificationService: INotificationService;

    public notifications: Notification[] = [];

    constructor(private notificationService: NotificationService) {
        //this.notificationService = notificationService;
    }

    //constructor(private notificationService: NotificationService) { }

    public ngOnInit() {
        this.notificationService.getNotification().subscribe((notification: Notification) => {
            if (!notification) {
                // clear notification when an empty alert is received
                this.notifications = [];

                return;
            }

            // add notification to array
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

        // return css class based on notification type
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