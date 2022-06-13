import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class VaultsService {
    async getVaultsByProfileId(id) {
        const res = await api.get(`api/profiles/${id}/vaults`)
        logger.log("[PROFILE VAULTS]", res.data)
        AppState.profileVaults = res.data
    }

    async getMyVaults(id) {
        const res = await api.get(`account/vaults`)
        AppState.myVaults = res.data
        return res.data
    }

    async create(newVault) {
        const res = await api.post('api/vaults', newVault)
        AppState.myVaults.unshift(res.data)
        return res.data
    }

    async remove(id) {
        await api.delete(`api/vaults/${id}`)
    }
}


export const vaultsService = new VaultsService()