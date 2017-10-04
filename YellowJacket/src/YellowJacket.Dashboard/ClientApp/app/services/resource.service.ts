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