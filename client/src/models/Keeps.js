

export class Keeps{
    constructor(data){
        this.id = data.id
        this.creatorId = data.creatorId
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.creator = data.creator
        this.kept = data.kept
        this.vaultKeepId = data.vaultKeepId || 0
        this.tagId = data.tagId || 0
    }
}

