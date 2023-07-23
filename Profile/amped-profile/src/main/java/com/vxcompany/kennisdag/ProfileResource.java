package com.vxcompany.kennisdag;

import io.quarkus.security.identity.SecurityIdentity;
import jakarta.annotation.security.RolesAllowed;
import jakarta.enterprise.context.RequestScoped;
import jakarta.inject.Inject;
import jakarta.transaction.Transactional;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.Context;
import jakarta.ws.rs.core.Response;
import jakarta.ws.rs.core.UriInfo;
import org.jboss.resteasy.reactive.NoCache;

import java.net.URI;

@Path("/profile")
@RolesAllowed("read:profile")
@RequestScoped
@NoCache
public class ProfileResource {
    @Inject
    SecurityIdentity securityIdentity;

    @GET
    @Path("/")
    public Response me() {
        String userId = securityIdentity.getPrincipal().getName();
        Profile profile = Profile.findByUserId(userId);

        if (profile == null) throw new NotFoundException("Unknown profile");

        return Response.ok(profile).build();
    }

    @GET
    @Path("/{nickName}")
    public Response user(String nickName) {
        Profile profile = Profile.findByNickName(nickName);

        if (profile == null) throw new NotFoundException("Unknown profile");

        return Response.ok(profile).build();
    }

    @Transactional
    @POST
    @Consumes("application/json")
    @Produces("application/json")
    @Path("/")
    @RolesAllowed("write:profile")
    public Response add(Profile profileToSave, @Context UriInfo uriInfo) {
        String userId = securityIdentity.getPrincipal().getName();
        Profile profile = new Profile();

        if (Profile.findByUserId(userId) == null) {
            profile.nickName = profileToSave.nickName;
            profile.bio = profileToSave.bio;
            profile.userId = userId;
            profile.persist();
        }

        URI uri = uriInfo.getAbsolutePathBuilder().path(profile.nickName).build();
        return Response.created(uri).entity(profile).build();
    }

    @Transactional
    @PUT
    @Consumes("application/json")
    @Path("/")
    @RolesAllowed("write:profile")
    public Response update(Profile profileToSave) {
        String userId = securityIdentity.getPrincipal().getName();
        Profile profile = Profile.findByUserId(userId);

        if (profile == null) throw new NotFoundException("Unknown profile");

        if (profileToSave.nickName != null) {
            profile.nickName = profileToSave.nickName;
        }

        if (profileToSave.bio != null) {
            profile.bio = profileToSave.bio;
        }

        return Response.ok().build();
    }
}
