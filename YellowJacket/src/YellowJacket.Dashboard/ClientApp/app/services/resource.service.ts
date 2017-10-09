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
    ErrorHappened,
    FieldIsRequired,
    FieldMaxCharacters
}

export type MessageContainer = { messageId: MessageId; value: string };

export interface IResourceService {
    getMessageWithArguments(message: MessageId, ...args: string[]): string;
    getMessage(message: MessageId): string;
}

@Injectable()
export class ResourceService implements IResourceService {
    private readonly messages: MessageContainer[];

    /**
     * Initialize a new instance of ResourceService.
     */
    constructor() {
        this.messages = new Array<MessageContainer>();

        this.messages.push({ messageId: MessageId.OperationSuccessfullyCompleted, value: 'Operation successfully completed' });
        this.messages.push({ messageId: MessageId.ErrorHappened, value: 'An error happened. Please check the log files.' });
        this.messages.push({ messageId: MessageId.FieldIsRequired, value: 'The field {0} is required.' });
        this.messages.push({ messageId: MessageId.FieldMaxCharacters, value: 'The field {0} must contains a maximum of {1} characters.' });
    }

    /**
     * Gets a message by his id.
     * @param {MessageId} messageId The message to get.
     * @param {string[]} args The message arguments.
     * @returns {string} The message.
     */
    public getMessageWithArguments(messageId: MessageId, ...args: string[]): string {
        let message = this.getMessage(messageId);

        if (message && args && args.length > 0) {
            for (let cpt = 0; cpt < args.length; cpt++) {
                message = message.replace(`{${cpt}}`, args[cpt]);
            }
        }

        return message ? message : '';
    }

    /**
 * Gets a message by his id.
 * @param {MessageId} messageId The message to get.
 * @param {string[]} args The message arguments.
 * @returns {string} The message.
 */
    public getMessage(messageId: MessageId): string {
        const messageContainer = this.messages.find(x => x.messageId === messageId);

        return messageContainer ? messageContainer.value : '';
    }
}