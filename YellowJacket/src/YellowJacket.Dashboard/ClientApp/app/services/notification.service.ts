import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';

import { Notification, NotificationType } from '../models/notification.model';

//export interface INotificationService {
//    getNotification(): Observable<any>,
//    success(message: string, keepAfterRouteChange: boolean): void;
//    error(message: string, keepAfterRouteChange: boolean): void;
//    info(message: string, keepAfterRouteChange: boolean): void;
//    warning(message: string, keepAfterRouteChange: boolean): void;
//    success(message: string, keepAfterRouteChange: boolean): void;
//}

@Injectable()
export class NotificationService {
    private subject = new Subject<Notification>();
    private keepAfterRouteChange = false;

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

    public getNotification(): Observable<any> {
        return this.subject.asObservable();
    }

    public success(message: string, keepAfterRouteChange = false) {
        this.notification(NotificationType.Success, message, keepAfterRouteChange);
    }

    public error(message: string, keepAfterRouteChange = false) {
        this.notification(NotificationType.Error, message, keepAfterRouteChange);
    }

    public info(message: string, keepAfterRouteChange = false) {
        this.notification(NotificationType.Info, message, keepAfterRouteChange);
    }

    public warning(message: string, keepAfterRouteChange = false) {
        this.notification(NotificationType.Warning, message, keepAfterRouteChange);
    }

    public notification(type: NotificationType, message: string, keepAfterRouteChange = false) {
        this.keepAfterRouteChange = keepAfterRouteChange;
        this.subject.next({ type: type, message: message } as Notification);
    }

    public clear() {
        this.subject.next();
    }
}