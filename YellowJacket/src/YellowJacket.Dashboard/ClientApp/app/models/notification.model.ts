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