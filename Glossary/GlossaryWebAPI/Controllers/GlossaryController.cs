using GlossaryDBContext.Repository;
using System.Collections.Generic;
using System.Web.Http;
using GlossaryWebAPI.Models;
using System.Net.Http;
using System;
using System.Net;
using System.Linq;

namespace GlossaryWebAPI.Controllers
{
    public class GlossaryController : ApiController
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork();

        [HttpGet]

        public virtual HttpResponseMessage GetTerms()
        {
            try
            {
                IQueryable<GlossaryDBContext.DBClasses.Glossary> glossary = _unitOfWork.ModelRepository.Get();
                List<GlossaryModel> glossaryModel = AutoMapper.Mapper.Map<List<GlossaryModel>>(glossary.OrderBy(p=>p.Term).ToList());
                var message = Request.CreateResponse(HttpStatusCode.OK, glossaryModel);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        [HttpGet]
        public HttpResponseMessage GetTermById(int id)
        {
            try
            {
                GlossaryDBContext.DBClasses.Glossary glossary = _unitOfWork.ModelRepository.GetByID(id);
                GlossaryModel glossaryModel = AutoMapper.Mapper.Map<GlossaryModel>(glossary);
                var message = Request.CreateResponse(HttpStatusCode.OK, glossaryModel);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        [HttpPost]
        public HttpResponseMessage SaveGlossary([FromBody] GlossaryModel glossaryModel)
        {
            try
            {
                var entity = AutoMapper.Mapper.Map<GlossaryDBContext.DBClasses.Glossary>(glossaryModel);
                _unitOfWork.ModelRepository.Insert(entity);
                _unitOfWork.Save();
                var insertedGlossary = _unitOfWork.ModelRepository.Get(termname => termname.Term == glossaryModel.Term);
                var message = Request.CreateResponse(HttpStatusCode.Created, insertedGlossary.ToList()[0]);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteGlossary(int id)
        {
            try
            {
                GlossaryDBContext.DBClasses.Glossary glossary = _unitOfWork.ModelRepository.GetByID(id);
                if(glossary== null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Term not found");
                }
                _unitOfWork.ModelRepository.Delete(id);
                _unitOfWork.Save();
                var message = Request.CreateResponse(HttpStatusCode.OK);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateGlossary(int id, [FromBody] GlossaryModel glossaryModel)
        {
            try
            {
                GlossaryDBContext.DBClasses.Glossary glossary = _unitOfWork.ModelRepository.GetByID(id);
                if (glossary == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Term not found");
                }
                glossaryModel.TermId = glossary.TermId;
                var entity = AutoMapper.Mapper.Map<GlossaryDBContext.DBClasses.Glossary>(glossaryModel);
                _unitOfWork.ModelRepository.Update(entity);
                _unitOfWork.Save();
                var message = Request.CreateResponse(HttpStatusCode.OK);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

    }
}
