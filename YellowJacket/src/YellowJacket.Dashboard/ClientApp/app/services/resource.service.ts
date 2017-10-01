import { Injectable } from '@angular/core';

export enum MessageId {
    OperationSuccessfullyCompleted,
    ErrorHappened
}

export type MessageContainer = { messageId: MessageId; value: string };

export interface IResourceService {
    getMessage(message: MessageId): string;
}

@Injectable()
export class ResourceService implements IResourceService{
    private readonly messages: MessageContainer[];

    constructor() {

        this.messages = new Array<MessageContainer>();

        this.messages.push({ messageId: MessageId.OperationSuccessfullyCompleted, value: 'Operation successfully completed' });
        this.messages.push({ messageId: MessageId.ErrorHappened, value: 'An error happened. Please check the log files.' }); 
    }

    public getMessage(message: MessageId): string {
        const messageContainer = this.messages.find(x => x.messageId === message);

        return messageContainer ? messageContainer.value : '';
    } 
}