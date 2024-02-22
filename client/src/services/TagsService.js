import { AppState } from "../AppState"
import { Tags } from "../models/Tags"
import { api } from "./AxiosService"


class TagsService{

    async createTag(tagData){
        let response = await api.post('api/tags', tagData)
        let newTag = new Tags(response.data)
        AppState.activeTag.push(newTag)
    }

    async getTagsByKeep(keepId){
        let response = await api.get(`api/tags/${keepId}`)
        let keepTags = await response.data.map(tag => new Tags(tag))
        AppState.activeTag = keepTags
    }

    async deleteTag(tagId){
        let response = await api.delete(`api/tags/${tagId}`)
        let tagIndex = AppState.activeTag.findIndex(tag => tag.id == tagId)
        AppState.activeTag.splice(tagIndex, 1)
        return response.data
    }
}

export const tagsService = new TagsService()