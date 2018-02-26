class MailBox{
    constructor(){
        this.mailbox = [];
        this._messageCount = 0;
    }

    addMessage(subject, text){
        let newMessage = {};
        if(typeof subject != 'string' || typeof text != 'string'){
            return;
        }
        newMessage.subject = subject;
        newMessage.text = text;
        this.mailbox.push(newMessage);
        this._messageCount++;
        return this;
    }

    get messageCount(){
        return this._messageCount;
    }

    deleteAllMessages(){
        this.mailbox = [];
        this._messageCount = 0;
    }

    findBySubject(substr){
        if(typeof substr != 'string' || this.mailbox.length == 0){
            return;
        }
        let selectedMessages = [];
        for(let message of this.mailbox){
            if(message.subject.indexOf(substr) != -1){
                selectedMessages.push(message);
            }
        }
        return selectedMessages;
    }

    toString(){
        if(this.mailbox.length == 0){
            return "* (empty mailbox)";
        }
        let result = [];
        for(let message of this.mailbox){
            result.push(`[${message.subject}] ${message.text}`);
        }

        return result.join('\n');
    }
}

let mb = new MailBox();
console.log("Msg count: " + mb.messageCount);
console.log('Messages:\n' + mb);
mb.addMessage("meeting", "Let's meet at 17/11");
mb.addMessage("beer", "Wanna drink beer tomorrow?");
mb.addMessage("question", "How to solve this problem?");
mb.addMessage("Sofia next week", "I am in Sofia next week.");
console.log("Msg count: " + mb.messageCount);
console.log('Messages:\n' + mb);
console.log("Messages holding 'rakiya': " +
    JSON.stringify(mb.findBySubject('rakiya')));
console.log("Messages holding 'ee': " +
    JSON.stringify(mb.findBySubject('ee')));

mb.deleteAllMessages();
console.log("Msg count: " + mb.messageCount);
console.log('Messages:\n' + mb);

console.log("New mailbox:\n" +
    new MailBox()
        .addMessage("Subj 1", "Msg 1")
        .addMessage("Subj 2", "Msg 2")
        .addMessage("Subj 3", "Msg 3")
        .toString());
