export class GenericSimpleResult {
    errors: string[];
    success: boolean;
}

export class GenericResult<TResult> extends GenericSimpleResult {
    public result: TResult;
}

export class SelectModel{
    public value:string;
    public label: string;
}
