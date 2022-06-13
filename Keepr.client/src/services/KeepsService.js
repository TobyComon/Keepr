import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class KeepsService {
    async getAll() {
        const res = await api.get('api/keeps')
        logger.log('[GET ALL KEEPS]', res.data)
        AppState.keeps = res.data
    }

    async getKeepById(keep) {
        const res = await api.get('api/keeps/' + keep.id)
        keep.views++
        AppState.activeKeep = keep
    }
    async getVaultKeeps(id) {
        const res = await api.get(`api/vaults/${id}/keeps`)
        logger.log("[VAULT KEEPS]", res.data)
        AppState.vaultKeeps = res.data
    }

    async getKeepsByProfileId(id) {
        const res = await api.get(`api/profiles/${id}/keeps`)
        logger.log("[PROFILE KEEPS]", res.data)
        AppState.profileKeeps = res.data
    }

    async create(newKeep) {
        const res = await api.post('api/keeps', newKeep)

        AppState.profileKeeps.unshift(res.data)
        return res.data
    }

    async remove(id) {
        await api.delete(`api/keeps/${id}`)
    }
}

export const keepsService = new KeepsService()