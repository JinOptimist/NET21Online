namespace WebPortal.DbStuff.Repositories.Interfaces;

public class CdekRepository : BaseRepository<Cdek>, ICdekRepository
{
    public CdekRepository(WebPortalContext portalContext) : base(portalContext)
    {
    }
    
    
}