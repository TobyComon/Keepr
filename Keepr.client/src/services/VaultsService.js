import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class VaultsService {
    async getVaultsByProfileId(id) {
        const res = await api.get(`api/profiles/${id}/vaults`)
        logger.log("[PROFILE VAULTS]", res.data)
        AppState.profileVaults = res.data
    }
}


export const vaultsService = new VaultsService()