import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class KeepsService {
    async getAll() {
        const res = await api.get('api/keeps')
        logger.log('[GET ALL KEEPS]', res.data)
        AppState.keeps = res.data
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
}

export const keepsService = new KeepsService()