export enum ResolutionEnum {
        New,
    Active,
    Unresolved,
    Resolved,
}
export enum BacklogPriorityEnum {
    VeryLow,
    Low,
    Medium,
    High,
    VeryHigh
}

export enum BacklogStatusEnum {
    Registered,
    ToDo,
    Done,
    NeedWorks,
    InProgress,
    Testing
}

export enum BacklogItemEnum {
    Epic,
    UserStory,
    Issue,
    Task,
    Bug,
    Features,
    Application
}


export namespace BacklogConstats {
    export function ResolutionType() {
        return Object.keys(ResolutionEnum).filter(
            (type) => isNaN(<any>type) && type !== 'values'
        );
    }

    export function BacklogPriorities() {
        return Object.keys(BacklogPriorityEnum).filter(
            (type) => isNaN(<any>type) && type !== 'values'
        );
    }

    export function BacklogStatus() {
        return Object.keys(BacklogStatusEnum).filter(
            (type) => isNaN(<any>type) && type !== 'values'
        );
    }

    export function BacklogItemType() {
        return Object.keys(BacklogItemEnum).filter(
            (type) => isNaN(<any>type) && type !== 'values'
        );
    }
}
