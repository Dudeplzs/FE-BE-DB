export class LogService{
    // tslint:disable-next-line: typedef
    logChange(name: string, status: boolean){
        console.log('Name as changed to: ' + name + ' And ' +
                    'Status as changed to: ' + status);
    }
}